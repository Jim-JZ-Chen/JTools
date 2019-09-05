using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class SettingWindow : EditorWindow
{
    Vector2 scrollPos;

    void OnGUI()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        Editor editor = Editor.CreateEditor(AssetDatabase.LoadMainAssetAtPath("Assets/JTools/Settings.asset"));
        editor.DrawDefaultInspector();

        EditorGUILayout.EndScrollView();

    }

    [MenuItem("Window/Settings #s")]
    public static void ShowWindow()
    {
        EditorWindow window = EditorWindow.GetWindow(typeof(SettingWindow));
        window.titleContent = new GUIContent("Settings");
    }
}