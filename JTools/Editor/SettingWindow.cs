using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

public class SettingWindow : EditorWindow
{
    void OnGUI()
    {
        Editor editor = Editor.CreateEditor(AssetDatabase.LoadMainAssetAtPath("Assets/JTools/Settings.asset"));
        editor.DrawDefaultInspector();
    }

    [MenuItem("Window/Settings #s")]
    public static void ShowWindow()
    {
        EditorWindow window = EditorWindow.GetWindow(typeof(SettingWindow));
        window.titleContent = new GUIContent("Settings");
    }
}


/*
    [MenuItem("Window/Settings1")]
    // Start is called before the first frame update
    public static void ShowSettingWindow()
    {
        Selection.activeObject = AssetDatabase.LoadMainAssetAtPath("Assets/JTools/Settings.asset");

        Type type = Assembly.GetAssembly(typeof(Editor)).GetType("UnityEditor.InspectorWindow");
        // Retrieve the existing Inspector tab, or create a new one if none is open
        EditorWindow inspectorWindow = EditorWindow.GetWindow(type);
        // Get the size of the currently window
        Vector2 size = new Vector2(inspectorWindow.position.width, inspectorWindow.position.height);
        // Clone the inspector tab (optionnal step)
        inspectorWindow = Instantiate(inspectorWindow);
        // Set min size, and focus the window
        //inspectorWindow.minSize = size;
        inspectorWindow.Show();
        
        //inspectorWindow.Focus();
        inspectorWindow.titleContent = new GUIContent("Settings");

        
        //lock the inspector
        PropertyInfo[] propertyInfos =  type.GetProperties();
        foreach (PropertyInfo info in propertyInfos)
        {
            Debug.Log(info.Name +" "+ info.PropertyType);
        }
        PropertyInfo propertyInfo = type.GetProperty("isLocked");
        //bool value = (bool)propertyInfo.GetValue(inspectorWindow, null);
        propertyInfo.SetValue(inspectorWindow, true, null);
        inspectorWindow.Repaint();
    }


    [MenuItem("Window/Settings2")]
    // Start is called before the first frame update
    public static void ShowSettingWindow2()
    {
        Selection.activeObject = AssetDatabase.LoadMainAssetAtPath("Assets/JTools/Settings.asset");
        
    }
    */
