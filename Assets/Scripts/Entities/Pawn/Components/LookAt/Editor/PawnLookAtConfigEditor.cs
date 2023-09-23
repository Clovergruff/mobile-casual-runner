using Gruffdev.BCSEditor;
using UnityEditor;

[CustomEditor(typeof(PawnLookAtConfig))]
public class PawnLookAtConfigEditor : EntityComponentEditorBase<PawnLookAtConfig>
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
