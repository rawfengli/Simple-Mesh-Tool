using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Codice.CM.Common.Selectors;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SimpleMesh))]
public class SimpleMeshDrawer : Editor
{
    public override void OnInspectorGUI()
    {
        SimpleMesh simpleMesh = (SimpleMesh)target;
        simpleMesh.mode = (SimpleMesh.MeshMode)EditorGUILayout.EnumPopup("Mesh Mode", simpleMesh.mode);
        switch (simpleMesh.mode)
        {
            case SimpleMesh.MeshMode.SimpleMode:

                SerializedProperty column = serializedObject.FindProperty("column");
                EditorGUILayout.PropertyField(column, new GUIContent("Column"));
                SerializedProperty row = serializedObject.FindProperty("row");
                EditorGUILayout.PropertyField(row, new GUIContent("Row")); 
                SerializedProperty length = serializedObject.FindProperty("length");
                EditorGUILayout.PropertyField(length, new GUIContent("Length")); 
                SerializedProperty width = serializedObject.FindProperty("width");
                EditorGUILayout.PropertyField(width, new GUIContent("Width"));

                if(GUILayout.Button("Square"))
                {
                    MeshBuilder.SimpleMeshPretreatmentSQ(simpleMesh);
                    MeshBuilder.CreateMesh(simpleMesh);
                }
                if(GUILayout.Button("Plane"))
                {
                    MeshBuilder.SimpleMeshPretreatmentPL(simpleMesh);
                    MeshBuilder.CreateMesh(simpleMesh);
                }
                break;

            case SimpleMesh.MeshMode.ExactMode:
                SerializedProperty vertices = serializedObject.FindProperty("vertices");
                EditorGUILayout.PropertyField(vertices, new GUIContent("Vertices"));
                SerializedProperty triangleIndexs = serializedObject.FindProperty("TriangleIndexs");
                EditorGUILayout.PropertyField(triangleIndexs, new GUIContent("TriangleIndexs"));
                SerializedProperty uv = serializedObject.FindProperty("uv");
                EditorGUILayout.PropertyField(uv, new GUIContent("uv"));

                if (GUILayout.Button("Create"))
                {
                    MeshBuilder.CreateMesh(simpleMesh);
                }

                break;
        }
        SerializedProperty m_Material = serializedObject.FindProperty("material");
        EditorGUILayout.PropertyField(m_Material, new GUIContent("Material"));
        serializedObject.ApplyModifiedProperties();
        
    }
}
