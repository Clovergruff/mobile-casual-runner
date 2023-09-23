using UnityEditor;
using UnityEngine;
using Gruffdev.BCSEditor;

[CustomEditor(typeof(PawnHealthConfig))]
public class PawnHealthConfigEditor : EntityComponentEditorBase<PawnHealthConfig>
{
	private readonly Color GRAYED_OUT_COLOR = new Color(0, 0, 0, 0.4f);
	private readonly byte HEALTH_LIMIT = 255;

	private Texture2D _hpSprite;
	private SerializedProperty _maxHealthProperty;
	private SerializedProperty _defaultHealthProperty;

	private GUIContent _healthGUIContent;
	private GUIStyle _healthIconStyle = GUIStyle.none;

	public static bool matchHealthValues;

	protected override void OnEnable()
	{
		base.OnEnable();

		_hpSprite = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Art/HealthIcon.png", typeof(Texture2D));
		_healthGUIContent = new GUIContent(_hpSprite);

		_maxHealthProperty = serializedObject.FindProperty("maxHealth");
		_defaultHealthProperty = serializedObject.FindProperty("defaultHealth");
	}

	public override void OnInspectorGUI()
	{
		using (var check = new EditorGUI.ChangeCheckScope())
		{
			matchHealthValues = EditorGUILayout.Toggle("Match min with max", matchHealthValues);

			var previousMaxValue = config.maxHealth;
			EditorGUILayout.PropertyField(_maxHealthProperty);
			var newMaxHealth = _maxHealthProperty.intValue;
			if (previousMaxValue != newMaxHealth && newMaxHealth < config.defaultHealth)
			{
				SetDefAndMaxHealth(newMaxHealth);
				newMaxHealth = _maxHealthProperty.intValue;
			}

			EditorGUI.indentLevel++;
			if (matchHealthValues)
			{
				if (previousMaxValue != newMaxHealth)
				{
					config.defaultHealth = newMaxHealth;
					_defaultHealthProperty.serializedObject.Update();
				}

			}
			else
			{
				if (config.defaultHealth > newMaxHealth)
					config.defaultHealth = newMaxHealth;
				
				var previousDefaultValue = config.defaultHealth;
				config.defaultHealth = EditorGUILayout.IntSlider("Default health", _defaultHealthProperty.intValue, 1, newMaxHealth);
				if (config.defaultHealth != previousDefaultValue)
				{
					_defaultHealthProperty.serializedObject.Update();
				}
			}
			EditorGUI.indentLevel--;

			GUILayout.Space(8);
			EditorGUILayout.BeginHorizontal();
			GUILayout.Space((EditorGUI.indentLevel + 1) * EditorExt.INDENTATION_WIDTH);
			
			Rect baseRect = GUILayoutUtility.GetRect(_healthGUIContent, _healthIconStyle, GUILayout.Width(16), GUILayout.Height(16));
			Rect rect = baseRect;
			float heartSpaceHeight = 0;
			for (byte i = 0; i < config.maxHealth; i++)
			{
				GUI.color = i >= config.defaultHealth
					? GRAYED_OUT_COLOR
					: Color.red;
				GUI.DrawTexture(rect, _hpSprite);
				GUI.color = Color.white;

				rect.x += 16;

				if (rect.x >= Screen.width - 20)
				{
					rect.x = baseRect.x;
					rect.y += 20;
					heartSpaceHeight += 20;
					Repaint();
				}
			}

			EditorGUILayout.EndHorizontal();
			GUILayoutUtility.GetRect(_healthGUIContent, _healthIconStyle, GUILayout.Width(16), GUILayout.Height(heartSpaceHeight));

			if (check.changed)
			{
				EditorUtility.SetDirty(config);
				serializedObject.ApplyModifiedProperties();
			}
		}
	}

	private void SetDefAndMaxHealth(int newUnifiedHealthValue)
	{
		config.maxHealth = newUnifiedHealthValue;
		config.defaultHealth = newUnifiedHealthValue;
		_defaultHealthProperty.serializedObject.Update();
		_maxHealthProperty.serializedObject.Update();
	}
}
