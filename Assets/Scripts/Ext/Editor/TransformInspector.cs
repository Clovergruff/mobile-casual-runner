using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

// Reverse engineered UnityEditor.TransformInspector

[CanEditMultipleObjects, CustomEditor(typeof(Transform))]
[Serializable]
public class TransformInspector : Editor
{
	public enum TransformMode
	{
		None = 0,
		Position = 1,
		Rotation = 2,
		Scale = 3,
	}
	public enum AxisType
	{
		X = 0,
		Y = 1,
		Z = 2,
	}
	public enum AxisMode
	{
		Local = 0,
		World = 1,
	}

	// 2D Mode
	[SerializeField]
	public static bool mode2D;
	public Vector2 position2D;
	public float rotation2D;
	public Vector2 scale2D;

	// Unified scale
	[SerializeField]
	public static bool unifiedScale;
	public float unifiedScaleValue = 1;

	// Original stuff
    private const float FIELD_WIDTH = 212.0f;
    private const bool WIDE_MODE = true;

    private const float POSITION_MAX = 100000.0f;

    private static GUIContent positionGUIContent = new GUIContent(LocalString("Position"), LocalString("The local position of this Game Object relative to the parent."));
    private static GUIContent rotationGUIContent = new GUIContent(LocalString("Rotation"), LocalString("The local rotation of this Game Object relative to the parent."));
    private static GUIContent scaleGUIContent = new GUIContent(LocalString("3D Scale"), LocalString("The local scaling of this Game Object relative to the parent."));

    private static string positionWarningText = LocalString("Due to floating-point precision limitations, it is recommended to bring the world coordinates of the GameObject within a smaller range.");

    private SerializedProperty positionProperty;
    private SerializedProperty rotationProperty;
    private SerializedProperty scaleProperty;

	// Controls
	SceneView sceneView;

	private Vector3 eulerAngles;
	private Event current;
	private Quaternion rotHelper;

	private TransformMode transformMode;
	private AxisType axisType;

	private static string LocalString(string text)
    {
		// Thanks, Team Unity :(
		return text; //LocalizationDatabase.GetLocalizedString(text);
    }

    public void OnEnable()
    {
        this.positionProperty = this.serializedObject.FindProperty("m_LocalPosition");
        this.rotationProperty = this.serializedObject.FindProperty("m_LocalRotation");
        this.scaleProperty = this.serializedObject.FindProperty("m_LocalScale");
    }

    public override void OnInspectorGUI()
    {
        EditorGUIUtility.wideMode = TransformInspector.WIDE_MODE;
        EditorGUIUtility.labelWidth = EditorGUIUtility.currentViewWidth - TransformInspector.FIELD_WIDTH; // align field to right of inspector

        this.serializedObject.Update();

		// 2D mode toggle
		string mode2DText = "3D Mode";
		if (mode2D)
			mode2DText = "2D Mode";

		if (GUILayout.Button(mode2DText, GUILayout.Width(64)))
			Toggle2DMode();

		// Position //
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("P", GUILayout.Width(20)))
			ResetPosition();

		if (mode2D)
		{
			Vector3 origPos = this.positionProperty.vector3Value;
			position2D = new Vector2(origPos.x, origPos.y);
			position2D = EditorGUILayout.Vector2Field("Position", position2D);
			this.positionProperty.vector3Value = new Vector3(position2D.x, position2D.y, this.positionProperty.vector3Value.z);
		}
		else
		{
			EditorGUILayout.PropertyField(this.positionProperty, positionGUIContent);
		}
		GUILayout.EndHorizontal();
		
		// Rotation //
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("R", GUILayout.Width(20)))
			ResetRotation();

		if (mode2D)
		{
			Vector3 euler = this.rotationProperty.quaternionValue.eulerAngles;
			rotation2D = euler.z;
			rotation2D = EditorGUILayout.FloatField("Rotation", rotation2D);
			this.rotationProperty.quaternionValue = Quaternion.Euler(euler.x, euler.y, rotation2D);
		}
		else
		{
			this.RotationPropertyField(this.rotationProperty, rotationGUIContent);
		}
		GUILayout.EndHorizontal();

		// Scale //
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("S", GUILayout.Width(20)))
			ResetScale();
		string uniScaleText = "M";
		if (unifiedScale)
			uniScaleText = "U";

		if (GUILayout.Button(uniScaleText, GUILayout.Width(20)))
			ToggleUnifiedScale();

		if (unifiedScale)
		{
			unifiedScaleValue = GetLargestScaleValue();
			unifiedScaleValue = EditorGUILayout.FloatField("1D Scale", unifiedScaleValue);
			this.scaleProperty.vector3Value = Vector3.one * unifiedScaleValue;
		}
		else
		{
			if (mode2D)
			{
				Vector3 origScale = this.scaleProperty.vector3Value;
				scale2D = new Vector2(origScale.x, origScale.y);
				scale2D = EditorGUILayout.Vector2Field("Scale", scale2D);
				this.scaleProperty.vector3Value = new Vector3(scale2D.x, scale2D.y, origScale.z);
			}
			else
			{
				EditorGUILayout.PropertyField(this.scaleProperty, scaleGUIContent);
			}
		}

		GUILayout.EndHorizontal();

        if (!ValidatePosition(((Transform)this.target).position))
        {
            EditorGUILayout.HelpBox(positionWarningText, MessageType.Warning);
        }

        this.serializedObject.ApplyModifiedProperties();
    }

	private void ToggleUnifiedScale()
	{
		unifiedScale = !unifiedScale;

		if (unifiedScale)
		{
			unifiedScaleValue = GetLargestScaleValue();
		}
		else
		{
			this.scaleProperty.vector3Value = Vector3.one * unifiedScaleValue;
		}
	}
	private float GetLargestScaleValue()
	{
		Vector3 sc = this.scaleProperty.vector3Value;
		float newUScale = sc.x;
		if (sc.y > newUScale)
			newUScale = sc.y;
		if (sc.z > newUScale)
			newUScale = sc.z;

		return newUScale;
	}
	public void Toggle2DMode()
	{
		mode2D = !mode2D;

		Vector3 pos = this.positionProperty.vector3Value;
		Vector3 scale = this.scaleProperty.vector3Value;
		Vector3 euler = this.rotationProperty.quaternionValue.eulerAngles;

		if (mode2D) // Set 2D Values
		{
			position2D = new Vector2(pos.x, pos.y);
			scale2D = new Vector2(scale.x, scale.y);
			rotation2D = euler.z;
		}
		else // Set 3D Values
		{
			this.positionProperty.vector3Value = new Vector3(position2D.x, position2D.y, pos.z);
			this.rotationProperty.quaternionValue = Quaternion.Euler(euler.x, euler.y, rotation2D);
			this.scaleProperty.vector3Value = new Vector3(scale2D.x, scale2D.y, scale.z);
		}
	}

	private void ResetPosition()
	{
		this.positionProperty.vector3Value = Vector3.zero;
	}
	private void ResetRotation()
	{
		this.rotationProperty.quaternionValue = Quaternion.identity;
	}
	private void ResetScale()
	{
		this.scaleProperty.vector3Value = Vector3.one;
	}

	private bool ValidatePosition(Vector3 position)
    {
        if (Mathf.Abs(position.x) > TransformInspector.POSITION_MAX) return false;
        if (Mathf.Abs(position.y) > TransformInspector.POSITION_MAX) return false;
        if (Mathf.Abs(position.z) > TransformInspector.POSITION_MAX) return false;
        return true;
    }

    private void RotationPropertyField(SerializedProperty rotationProperty, GUIContent content)
    {
        Transform transform = (Transform)this.targets[0];
        Quaternion localRotation = transform.localRotation;
        foreach (UnityEngine.Object t in (UnityEngine.Object[])this.targets)
        {
            if (!SameRotation(localRotation, ((Transform)t).localRotation))
            {
                EditorGUI.showMixedValue = true;
                break;
            }
        }

        EditorGUI.BeginChangeCheck();

        Vector3 eulerAngles = EditorGUILayout.Vector3Field(content, localRotation.eulerAngles);

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObjects(this.targets, "Rotation Changed");
            foreach (UnityEngine.Object obj in this.targets)
            {
                Transform t = (Transform)obj;
                t.localEulerAngles = eulerAngles;
            }
            rotationProperty.serializedObject.SetIsDifferentCacheDirty();
        }

        EditorGUI.showMixedValue = false;
    }

    private bool SameRotation(Quaternion rot1, Quaternion rot2)
    {
        if (rot1.x != rot2.x) return false;
        if (rot1.y != rot2.y) return false;
        if (rot1.z != rot2.z) return false;
        if (rot1.w != rot2.w) return false;
        return true;
    }
}