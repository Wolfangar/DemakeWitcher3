using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(CombatGrid))]
[CanEditMultipleObjects]
public class CombatGridEditor : Editor
{
    SerializedProperty myCellTemplateProperty;
    SerializedProperty lookAtPoint;
    string[,] myGridCache = new string[1, 1];
    SerializedProperty myWidthProperty;
    SerializedProperty myHeightProperty;
    SerializedProperty myTerrainSpriteLibraryProperty;
    private bool myIsDirty = false;

    private int myWidth;
    private int myHeight;

    void OnEnable()
    {
        lookAtPoint = serializedObject.FindProperty("derp2");
        myWidthProperty = serializedObject.FindProperty("myTargetWidth");
        myHeightProperty = serializedObject.FindProperty("myTargetHeight");
        myCellTemplateProperty = serializedObject.FindProperty("myCellTemplate");
        myTerrainSpriteLibraryProperty = serializedObject.FindProperty("myTerrainSpriteLibrary");
        myGridCache = new string[1, 1];
    }

    private void LoadGridFromObject(CombatGrid grid)
    {
        int newGridWidth = Mathf.Max(myWidth, 0);
        int newGridHeight = Mathf.Max(myHeight, 0);
        int currentCacheWidth = myGridCache.GetLength(1);
        int currentCacheHeight = myGridCache.GetLength(0);

        if (newGridWidth == currentCacheWidth && newGridHeight == currentCacheHeight)
        {
            //return;
        }

        string defaultTerrain = "G";
        int newCacheWidth = Mathf.Max(newGridWidth, currentCacheWidth);
        int newCacheHeight = Mathf.Max(newGridHeight, currentCacheHeight);

        string[,] newGridCache = new string[newCacheHeight, newCacheWidth];

        // Preserve original cache
        for (int y = 0; y < currentCacheHeight; ++y)
        {
            for (int x = 0; x < currentCacheWidth; ++x)
            {
                if (x >= newCacheWidth && y >= newCacheHeight)
                    continue;

                newGridCache[y, x] = myGridCache[y, x];
            }
        }
 
        for (int y = 0; y < newGridHeight; ++y)
        {
            for (int x = 0; x < newGridWidth; ++x)
            {
                bool isCachedValueValid = x < myGridCache.GetLength(1) && y < myGridCache.GetLength(0);
                string previousTerrain = isCachedValueValid ? myGridCache[y, x] : defaultTerrain;
                bool isValid = grid.IsValidCoord(x, y);

                string value = isValid ? grid.GetTerrainCellAt(x, y) : previousTerrain;
                newGridCache[y, x] = value;
            }
        }

        myGridCache = newGridCache;
    }
    
    public override void OnInspectorGUI()
    {
        Debug.Log("OnInspectorGUI() -- Start");
        serializedObject.Update();

        /*myCombatGridTerrain = (target as CombatGrid).myTerrainGrid;
        if (myCombatGridTerrain == null)
        {
            myCombatGridTerrain = new string[1, 1];
        }*/

        int newWidth = EditorGUILayout.IntField("Width", myWidth);
        int newHeight = EditorGUILayout.IntField("Height", myHeight);
        EditorGUILayout.PropertyField(myCellTemplateProperty);
        EditorGUILayout.PropertyField(myTerrainSpriteLibraryProperty, true);

        newWidth = Mathf.Max(newWidth, 0);
        newHeight = Mathf.Max(newHeight, 0);

        if (newWidth != myWidth)
        {
            myIsDirty = true;
            myWidth = newWidth;
        }

        if (newHeight != myHeight)
        {
            myIsDirty = true;
            myHeight = newHeight;
        }

        CombatGrid grid = (target as CombatGrid);
        LoadGridFromObject(grid);
        
        for (int y = 0; y < myHeight; ++y)
        {
            GUILayout.BeginHorizontal();
            for (int x = 0; x < myWidth; ++x)
            {
                string previousValue = grid.ValidateValue(myGridCache[y, x]);
                string newValue = EditorGUILayout.TextField(previousValue, GUILayout.Width(20));
                if (newValue != myGridCache[y, x])
                {
                    Debug.Log("IS DIRTY");
                    myIsDirty = true;
                }
                myGridCache[y, x] = newValue;
            }
            GUILayout.EndHorizontal();
        }

        grid.SetTerrainGrid(myGridCache, myWidth, myHeight);

        serializedObject.ApplyModifiedProperties();
        Debug.Log("OnInspectorGUI() -- End");
    }

    public void OnSceneGUI()
    {
        Debug.Log("OnSceneGUI() -- Start");
        CombatGrid grid = (target as CombatGrid);

//        myIsDirty |= grid.IsMySizeDirty();
        
        if (myIsDirty)
        {
            Debug.Log("Grid is dirty");
            Undo.RecordObject(grid, "Combat Grid Change");
            //grid.myTerrainGrid = myCombatGridTerrain;
            grid.UpdateGrid();
        }

        myIsDirty = false;
        Debug.Log("OnSceneGUI() -- End");
    }
}