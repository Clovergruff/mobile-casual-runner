using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PawnHealthSystem))]
public class PawnHealthSystemEditor : Editor
{
	private PawnHealthSystem system;

	private SerializedProperty _configProperty;
	private SerializedProperty _healthProperty;

	private void OnEnable()
	{
		if (target != null)
			system = (PawnHealthSystem)target;

		_configProperty = serializedObject.FindProperty("config");
		_healthProperty = serializedObject.FindProperty("health");
	}

	public override void OnInspectorGUI()
	{
		EditorGUILayout.PropertyField(_configProperty);
		EditorGUILayout.PropertyField(_healthProperty);

		EditorGUILayout.Space();
		if (GUILayout.Button("Kill"))
		{
			system.TakeDamage(1337);
		}
	}
}
