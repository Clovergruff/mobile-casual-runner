using UnityEditor;
using Gruffdev.BCSEditor;

[CustomEditor(typeof(PickupStatsConfig))]
public class PickupStatsConfigEditor : EntityComponentEditorBase<PickupStatsConfig>
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
