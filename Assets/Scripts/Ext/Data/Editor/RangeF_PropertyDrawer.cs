using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(RangeF))]
public class RangeF_PropertyDrawer : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty(position, label, property);

		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

		float originalLabelWidth = EditorGUIUtility.labelWidth;

		EditorGUIUtility.labelWidth = 30;

		position.width = 90;
		EditorGUI.PropertyField(position, property.FindPropertyRelative("min"));

		position.x += 100;
		EditorGUI.PropertyField(position, property.FindPropertyRelative("max"));

		EditorGUIUtility.labelWidth = originalLabelWidth;

		EditorGUI.EndProperty();
	}
}