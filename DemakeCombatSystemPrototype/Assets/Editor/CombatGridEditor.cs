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
    string[,] myCombatGridTerrain;
    SerializedProperty myWidthProperty;
    SerializedProperty myHeightProperty;
    SerializedProperty myTerrainSpriteLibraryProperty;
    private bool myIsDirty = false;

    void OnEnable()
    {
        lookAtPoint = serializedObject.FindProperty("derp2");
        myWidthProperty = serializedObject.FindProperty("myWidth");
        myHeightProperty = serializedObject.FindProperty("myHeight");
        myCellTemplateProperty = serializedObject.FindProperty("myCellTemplate");
        myTerrainSpriteLibraryProperty = serializedObject.FindProperty("myTerrainSpriteLibrary");
        myCombatGridTerrain = new string[1, 1];
    }

    private void UpdateGridSize(int width, int height)
    {
        if (width == myCombatGridTerrain.GetLength(1) && height == myCombatGridTerrain.GetLength(0))
        {
            return;
        }

        string defaultTerrain = "G";
        string[,] newGrid = new string[height, width];
        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                bool isValid = x < myCombatGridTerrain.GetLength(1) && y < myCombatGridTerrain.GetLength(0);
                string value = isValid ? myCombatGridTerrain[y, x] : defaultTerrain;
                newGrid[y, x] = value;
            }
        }

        myCombatGridTerrain = newGrid;
    }
    
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        myCombatGridTerrain = (target as CombatGrid).myTerrainGrid;
        if (myCombatGridTerrain == null)
        {
            myCombatGridTerrain = new string[1, 1];
        }

        EditorGUILayout.PropertyField(myWidthProperty);
        EditorGUILayout.PropertyField(myHeightProperty);
        EditorGUILayout.PropertyField(myCellTemplateProperty);
        EditorGUILayout.PropertyField(myTerrainSpriteLibraryProperty, true);

        CombatGrid grid = (target as CombatGrid);
        int width = Mathf.Max(grid.myWidth, 0);
        int height = Mathf.Max(grid.myHeight, 0);

        UpdateGridSize(width, height);
        //myCombatGridTerrain = new string[height, width];
        
        EditorGUILayout.PropertyField(lookAtPoint);
        
        for (int y = 0; y < height; ++y)
        {
            GUILayout.BeginHorizontal();
            for (int x = 0; x < width; ++x)
            {
                //GUILayout.TextField(myCombatGridTerrain[y, x], GUILayout.Width(20));
                string newValue = EditorGUILayout.TextField(myCombatGridTerrain[y, x], GUILayout.Width(20));
                if (newValue != myCombatGridTerrain[y, x])
                {
                    myIsDirty = true;
                }
                myCombatGridTerrain[y, x] = newValue;
            }
            GUILayout.EndHorizontal();
        }

        (target as CombatGrid).myTerrainGrid = myCombatGridTerrain;

        serializedObject.ApplyModifiedProperties();
    }

    public void OnSceneGUI()
    {
        CombatGrid grid = (target as CombatGrid);
        int width = grid.myWidth;
        int height = grid.myHeight;

        grid.UpdateGrid(width, height, myIsDirty);

        myIsDirty = false;
    }
}