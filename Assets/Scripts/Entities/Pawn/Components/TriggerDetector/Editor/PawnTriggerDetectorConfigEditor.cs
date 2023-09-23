using UnityEditor;
using Gruffdev.BCSEditor;

[CustomEditor(typeof(PawnTriggerDetectorConfig))]
public class PawnTriggerDetectorConfigEditor : EntityComponentEditorBase<PawnTriggerDetectorConfig>
{

	protected override void OnEnable()
	{
		base.OnEnable();
	}

	public override void OnInspectorGUI()
	{
		using (var check = new EditorGUI.ChangeCheckScope())
		{
			base.OnInspectorGUI();
			
			if (check.changed)
			{
				EditorUtility.SetDirty(config);
				serializedObject.ApplyModifiedProperties();
			}
		}
	}
}
