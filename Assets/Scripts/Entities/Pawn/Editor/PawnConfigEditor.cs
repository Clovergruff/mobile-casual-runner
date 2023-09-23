using UnityEditor;
using Gruffdev.BCSEditor;
using UnityEngine;

[CustomEditor(typeof(PawnConfig))]
public class PawnConfigEditor : EntityConfigAssetEditorBase<PawnComponentConfig, PawnConfig>
{
	public SerializedProperty _typeProperty;

	protected override void OnEnable()
	{
		base.OnEnable();

		_typeProperty = serializedObject.FindProperty("type"); 
	}

	public override void OnInspectorGUI()
	{
		using (var check = new EditorGUI.ChangeCheckScope())
		{
			EditorGUILayout.PropertyField(_typeProperty);
			EditorGUILayout.Space();

			DrawComponentList();

			if (check.changed)
			{
				EditorUtility.SetDirty(entityConfigAsset);
				serializedObject.ApplyModifiedProperties();
			}
		}
	}
}
