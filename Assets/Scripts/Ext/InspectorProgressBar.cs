using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Reflection;

[System.AttributeUsage(System.AttributeTargets.Field)]
public class ProgressBarAttribute : PropertyAttribute
{
	public readonly float min;
	public readonly float max;
	public readonly bool allowEdit;

	public ProgressBarAttribute(float min = 0, float max = 1, bool allowEdit = false)
	{
		this.min = min;
		this.max = max;
		this.allowEdit = allowEdit;
	}
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ProgressBarAttribute))]
public class ProgressBarPropertyDrawer : PropertyDrawer
{
	private MethodInfo _eventMethodInfo = null;
	private float newValue;

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		if (property.propertyType != SerializedPropertyType.Float && property.propertyType != SerializedPropertyType.Integer)
		{
			EditorGUI.LabelField(position, label.text, "Use Range with float or int.");
		}
		else
		{
			EditorGUI.BeginProperty(position, label, property);
			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
			float originalLabelWidth = EditorGUIUtility.labelWidth;

			ProgressBarAttribute progressBarAttribute = (ProgressBarAttribute)attribute;

			Rect barRect;

			if (progressBarAttribute.allowEdit)
			{
				barRect = new Rect(position.x + 80, position.y, position.width - 80, position.height);
				newValue = EditorGUI.FloatField(new Rect(position.x, position.y, 60, position.height), newValue);

				if (property.propertyType == SerializedPropertyType.Float)
					property.floatValue = newValue;
				else
					property.intValue = Mathf.RoundToInt(newValue);
			}
			else
			{
				 barRect = new Rect(position.x, position.y, position.width, position.height);
			}

			float propVal = 0;

			if (property.propertyType == SerializedPropertyType.Float)
				propVal = property.floatValue;
			else if (property.propertyType == SerializedPropertyType.Integer)
				propVal = property.intValue;
			float val = Mathf.InverseLerp(progressBarAttribute.min, progressBarAttribute.max, propVal);

			EditorGUI.ProgressBar(barRect, val, $"{(val * 100).ToString("F0")}%");

			EditorGUIUtility.labelWidth = originalLabelWidth;
			EditorGUI.EndProperty();
		}
	}
}
#endif
