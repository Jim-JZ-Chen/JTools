using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;





[CustomPropertyDrawer(typeof(EnumArrayAttribute))]
public class NamedArrayDrawer : PropertyDrawer
{
    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
    {
        try
        {
            int pos = int.Parse(property.propertyPath.Split('[', ']')[1]);
            EditorGUI.PropertyField(rect, property, new GUIContent(pos + ". " +((EnumArrayAttribute)attribute).names[pos]));
        }
        catch
        {
            //EditorGUI.PropertyField(rect, property, label);
        }
    }
}
