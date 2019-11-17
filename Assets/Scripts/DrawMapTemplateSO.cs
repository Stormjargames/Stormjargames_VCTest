using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//Editor/DrawScript.cs
[CustomEditor(typeof(MapTemplateSO))]
public class DrawMapTemplateSO : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //Cast target to MapTemplateSO
        MapTemplateSO m = (MapTemplateSO)target;

        //Never let user go below 1 w/h
        m.height = Mathf.Max(1, EditorGUILayout.IntField("Height:", m.height));
        m.width = Mathf.Max(1, EditorGUILayout.IntField("Width:", m.width));

        //Check that the array sizes match w/h values
        CheckArraySizes(m);

        //Draw Label

        GUILayout.BeginHorizontal();
        if(GUILayout.Button("Generate Random Map"))
        {
            m.RandomGen();
        }
        if (GUILayout.Button("All Town"))
        {
            m.ChangeAllTiles(TileType.TOWN);
        }
        if (GUILayout.Button("All Cathedral"))
        {
            m.ChangeAllTiles(TileType.CATHEDRAL);
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("");
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        m.TemplateName = EditorGUILayout.TextField("Template Name:", m.TemplateName);
        GUILayout.EndHorizontal();
        GUILayout.BeginVertical();
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("Template Elements");
        EditorGUILayout.Separator();
        GUILayout.EndVertical();

        //Draw popups
        for (int i = 0; i < m.myThings.Length; i++)
        {
            GUILayout.BeginHorizontal();
            for (int j = 0; j < m.myThings[i].entries.Length; j++)
            {
                GUI.backgroundColor = SetColour(m.myThings[i].entries[j]);
                m.myThings[i].entries[j] = (TileType)EditorGUILayout.EnumPopup(m.myThings[i].entries[j]);
                EditorGUILayout.Separator();
            }

            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            EditorGUILayout.Separator();
            GUILayout.EndHorizontal();
        }

        //GUILayout.BeginHorizontal();

        //SerializedObject so = base.serializedObject;
        //so.UpdateIfRequiredOrScript();

        //EditorGUILayout.PropertyField(so.FindProperty("prefabs"), false);

        //so.ApplyModifiedProperties();

        //GUILayout.EndHorizontal();
    }

    public Color SetColour(TileType t)
    {
        switch (t)
        {
            case TileType.CATHEDRAL: return Color.red;
            case TileType.NULL: return Color.white;
            case TileType.TOWN: return Color.green;
        }
        return Color.white;
    }


    void CheckArraySizes(MapTemplateSO m)
    {
        if (m.myThings == null ||
            m.myThings.Length == 0 ||
            m.myThings[0] == null ||
            m.myThings[0].entries.Length == 0)
        {
            //Create/init new array when there isn't one
            m.myThings = new MapTemplateSO.Row[m.height];
            for (int i = 0; i < m.myThings.Length; i++)
            {
                m.myThings[i] = new MapTemplateSO.Row();
                m.myThings[i].entries = new TileType[m.width];
            }
        }
        else if (m.myThings.Length != m.height)
        {
            //resizing number of rows
            int oldHeight = m.myThings.Length;
            bool growing = m.height > m.myThings.Length;
            System.Array.Resize(ref m.myThings, m.height);
            if (growing)
            {
                //Add new rows to array when growing array
                for (int i = oldHeight; i < m.height; i++)
                {
                    m.myThings[i] = new MapTemplateSO.Row();
                    m.myThings[i].entries = new TileType[m.width];
                }
            }

        }
        else if (m.myThings[0].entries.Length != m.width)
        {
            //resizing number of entries per row
            for (int i = 0; i < m.myThings.Length; i++)
            {
                System.Array.Resize(ref m.myThings[i].entries, m.width);
            }
        }
    }
}