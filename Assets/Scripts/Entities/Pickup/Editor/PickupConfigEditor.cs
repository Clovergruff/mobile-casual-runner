using UnityEditor;
using Gruffdev.BCSEditor;

[CustomEditor(typeof(PickupConfig))]
public class PickupConfigEditor : EntityConfigAssetEditorBase<PickupComponentConfig, PickupConfig>
{
	protected override void OnEnable()
	{
		base.OnEnable();
	}

	public override void OnInspectorGUI()
	{
		using (var check = new EditorGUI.ChangeCheckScope())
		{
			DrawComponentList();

			if (check.changed)
			{
				EditorUtility.SetDirty(entityConfigAsset);
				serializedObject.ApplyModifiedProperties();
			}
		}
	}
}
