using UnityEngine;
using UnityEditor;
using Gruffdev.BCSEditor;

[CustomEditor(typeof(PawnSkinData))]
public class PawnSkinDataEditor : Editor
{
	private PawnSkinData data;

	private void OnEnable()
	{
		if (target != null)
			data = (PawnSkinData)target;
	}

	public override void OnInspectorGUI()
	{
		using (var check = new EditorGUI.ChangeCheckScope())
		{
			data.prefab = EditorExt.ObjectFieldWithPreview(data.prefab, 64);

			if (check.changed)
			{
				EditorUtility.SetDirty(data);
				serializedObject.ApplyModifiedProperties();
			}
		}
	}
}
