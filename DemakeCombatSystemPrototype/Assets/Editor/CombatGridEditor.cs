using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CombatGrid))]
[CanEditMultipleObjects]
public class CombatGridEditor : Editor
{
    SerializedProperty myTerrainSpriteLibraryProperty;
    SerializedProperty myTerrainGridDataProperty;
    SerializedProperty myCellTemplateProperty;

    void OnEnable()
    {   
        myTerrainSpriteLibraryProperty = serializedObject.FindProperty("myTerrainSpriteLibrary");
        myTerrainGridDataProperty = serializedObject.FindProperty("myTerrainGridData");
        myCellTemplateProperty = serializedObject.FindProperty("myCellTemplate");
    }
    
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        CombatGrid grid = (target as CombatGrid);
        if (grid.myTerrainGridData != null)
        {
            grid.myTerrainGridData.SetGridChangeListener(delegate ()
            {
                Debug.Log("Combat Grid got updated from ScriptableObject!");
                grid.UpdateGridRepresentation();
            });
        }

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(myTerrainSpriteLibraryProperty);
        EditorGUILayout.PropertyField(myTerrainGridDataProperty);
        bool wasChanged = EditorGUI.EndChangeCheck();

        EditorGUILayout.PropertyField(myCellTemplateProperty);

        serializedObject.ApplyModifiedProperties();

        if (wasChanged || GUILayout.Button("Rebuild cells preview"))
        {
            grid.UpdateGridRepresentation();
        }
    }
}