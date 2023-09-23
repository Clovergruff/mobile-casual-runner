using UnityEditor;
using Gruffdev.BCSEditor;

[CustomEditor(typeof(PawnPhysicsConfig))]
public class PawnPhysicsConfigEditor : EntityComponentEditorBase<PawnPhysicsConfig>
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
