using UnityEditor;
using Gruffdev.BCSEditor;

[CustomEditor(typeof(PickupEventsConfig))]
public class PickupEventsConfigEditor : EntityComponentEditorBase<PickupEventsConfig>
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
