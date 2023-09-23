using UnityEditor;
using Gruffdev.BCSEditor;

[CustomEditor(typeof(PawnGroundDetectorConfig))]
public class PawnGroundDetectorConfigEditor : EntityComponentEditorBase<PawnGroundDetectorConfig>
{
	public SerializedProperty _alignToGroundProperty;
	public SerializedProperty _lockToGroundProperty;
	public SerializedProperty _groundLayersProperty;
	public SerializedProperty _slideOffSteepSurfacesProperty;
	public SerializedProperty _slidingSurfaceAngleProperty;

	protected override void OnEnable()
	{
		base.OnEnable();

		_alignToGroundProperty = serializedObject.FindProperty("alignToGround"); 
		_lockToGroundProperty = serializedObject.FindProperty("lockToGround"); 
		_groundLayersProperty = serializedObject.FindProperty("groundLayers"); 
		_slideOffSteepSurfacesProperty = serializedObject.FindProperty("slideOffSteepSurfaces"); 
		_slidingSurfaceAngleProperty = serializedObject.FindProperty("slidingSurfaceAngle"); 
	}

	public override void OnInspectorGUI()
	{
		using (var check = new EditorGUI.ChangeCheckScope())
		{
			EditorGUILayout.PropertyField(_alignToGroundProperty);
			EditorGUILayout.PropertyField(_lockToGroundProperty);
			EditorGUILayout.PropertyField(_groundLayersProperty);
			EditorGUILayout.PropertyField(_slideOffSteepSurfacesProperty);
			if (config.slideOffSteepSurfaces)
			{
				EditorGUI.indentLevel++;
				EditorGUILayout.PropertyField(_slidingSurfaceAngleProperty);
				EditorGUI.indentLevel--;
			}
			
			if (check.changed)
			{
				EditorUtility.SetDirty(config);
				serializedObject.ApplyModifiedProperties();
			}
		}
	}
}
