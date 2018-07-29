using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TerrainGridData))]
[CanEditMultipleObjects]
public class TerrainGridDataEditor : Editor
{
    string[,] myGridCache;

    private bool myIsDirty = false;

    void OnEnable()
    {
        myGridCache = new string[1, 1];
    }

    private void UpdateGridCache(string[,] aSource2DGrid, int newWidth = -1, int newHeight = -1)
    {
        int currentCacheWidth = myGridCache.GetLength(1);
        int currentCacheHeight = myGridCache.GetLength(0);
        int sourceWidth = aSource2DGrid.GetLength(1);
        int sourceHeight = aSource2DGrid.GetLength(0);

        if (newWidth < 0)
            newWidth = sourceWidth;
        if (newHeight < 0)
            newHeight = sourceHeight;

        if (newWidth <= currentCacheWidth && newHeight <= currentCacheHeight)
        {
            return;
        }

        string defaultTerrain = "G";
        string[,] newGridCache = new string[newHeight, newWidth];

        // Copy original cache into the larger one
        // and use default for the new cells
        for (int y = 0; y < newHeight; ++y)
        {
            for (int x = 0; x < newWidth; ++x)
            {
                bool isSourceValid = x < sourceWidth && y < sourceHeight;
                string value = isSourceValid ? aSource2DGrid[y, x] : defaultTerrain;
                newGridCache[y, x] = value;
            }
        }

        myGridCache = newGridCache;
    }

    public override void OnInspectorGUI()
    {
        bool debug = false;

        serializedObject.Update();

        TerrainGridData gridData = (target as TerrainGridData);

        if (debug) Debug.Log("STEP 1 W=" + gridData.myWidth + " / H=" + gridData.myHeight);

        string[,] data = gridData.Make2DGrid();
        UpdateGridCache(data);

        if (myGridCache == null)
        {
            myGridCache = new string[1, 1];
        }

        int width = EditorGUILayout.IntField("Width", gridData.myWidth);
        int height = EditorGUILayout.IntField("Height", gridData.myHeight);

        width = Mathf.Max(width, 0);
        height = Mathf.Max(height, 0);

        if (width != gridData.myWidth)
            myIsDirty = true;

        if (height != gridData.myHeight)
            myIsDirty = true;

        if (debug) Debug.Log("STEP 2 W=" + gridData.myWidth + " / H=" + gridData.myHeight);
        
        UpdateGridCache(myGridCache, width, height);

        for (int y = 0; y < height; ++y)
        {
            GUILayout.BeginHorizontal();
            for (int x = 0; x < width; ++x)
            {
                string newValue = EditorGUILayout.TextField(myGridCache[y, x], GUILayout.Width(20));
                if (newValue != myGridCache[y, x])
                {
                    myIsDirty = true;
                }
                myGridCache[y, x] = newValue;
            }
            GUILayout.EndHorizontal();
        }

        if (myIsDirty)
        {
            gridData.LoadFrom2DGrid(myGridCache, width, height);
            myIsDirty = false;
        }

        if (debug) Debug.Log("STEP 3 W=" + gridData.myWidth + " / H=" + gridData.myHeight);

        serializedObject.ApplyModifiedProperties();
        EditorUtility.SetDirty(gridData);
    }
}
