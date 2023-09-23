using UnityEditor;
using Gruffdev.BCSEditor;

[CustomEditor(typeof(PawnRotatorConfig))]
public class PawnRotatorConfigEditor : EntityComponentEditorBase<PawnRotatorConfig>
{
	public SerializedProperty _rotationModeProperty;
	public SerializedProperty _incrementSizeProperty;
	public SerializedProperty _defaultSpeedProperty;
	public SerializedProperty _defaultMaxSpeedProperty;

	protected override void OnEnable()
	{
		base.OnEnable();

		_rotationModeProperty = serializedObject.FindProperty("rotationMode"); 
		_incrementSizeProperty = serializedObject.FindProperty("incrementSize"); 
		_defaultSpeedProperty = serializedObject.FindProperty("defaultSpeed"); 
		_defaultMaxSpeedProperty = serializedObject.FindProperty("defaultMaxSpeed"); 
	}

	public override void OnInspectorGUI()
	{
		using (var check = new EditorGUI.ChangeCheckScope())
		{
			EditorGUILayout.PropertyField(_rotationModeProperty);
			if (config.rotationMode == PawnRotatorConfig.RotationModeType.Incremental)
			{
				EditorGUI.indentLevel++;
				EditorGUILayout.PropertyField(_incrementSizeProperty);
				EditorGUI.indentLevel--;
			}
			EditorGUILayout.PropertyField(_defaultSpeedProperty);
			EditorGUILayout.PropertyField(_defaultMaxSpeedProperty);
			
			if (check.changed)
			{
				EditorUtility.SetDirty(config);
				serializedObject.ApplyModifiedProperties();
			}
		}
	}
}
