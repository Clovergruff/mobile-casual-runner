using UnityEngine;
using UnityEditor;
using System;
using UnityEditor.Experimental.SceneManagement;

public class ObjectReplacerEditor : EditorWindow
{
	[SerializeField] private GameObject prefab;
	[SerializeField] private bool onlyActiveObjects;

	[SerializeField] private bool prefabPosition = false;
	[SerializeField] private bool prefabRotation = false;
	[SerializeField] private bool prefabScale = true;

	[SerializeField] private GameObject oldPrefab;

	private Texture2D previewTex = null;

	[MenuItem("Tools/Object replacer")]
	static void CreateObjectReplacerEditor()
	{
		var win = GetWindow<ObjectReplacerEditor>();
		win.maxSize = new Vector2(250, 350);
	}

	private bool PrefabIsValid(GameObject selected)
	{
		if (!selected || PrefabUtility.IsPartOfPrefabAsset(selected))
		{
			return false;
		}

		if (!selected.activeInHierarchy && onlyActiveObjects || !prefab)
		{
			return false;
		}

		var prefabType = PrefabUtility.GetPrefabAssetType(prefab);

		if (prefabType != PrefabAssetType.Regular &&
			prefabType != PrefabAssetType.Model &&
			prefabType != PrefabAssetType.Variant)
		{
			return false;
		}

		return true;
	}

	private void OnGUI()
	{
		prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", prefab, typeof(GameObject), false);
		onlyActiveObjects = EditorGUILayout.Toggle("Only active objects", onlyActiveObjects);
		prefabPosition = EditorGUILayout.Toggle("Use prefab Position", prefabPosition);
		prefabRotation = EditorGUILayout.Toggle("Use prefab Rotation", prefabRotation);
		prefabScale = EditorGUILayout.Toggle("Use prefab Scale", prefabScale);

		if (oldPrefab != prefab && prefab != null)
		{
			previewTex = RuntimePreviewGenerator.GenerateModelPreview(prefab.transform, 256, 256);
		}

		if (prefab == null)
			GUI.enabled = false;

		bool prefabSelected = false;
		var selection = Selection.gameObjects;

		for (var i = selection.Length - 1; i >= 0; --i)
		{
			if (PrefabIsValid(selection[i]))
			{
				prefabSelected = true;
				break;
			}
		}

		GUI.enabled = prefabSelected;
		if (GUILayout.Button("Replace"))
		{
			for (var i = selection.Length - 1; i >= 0; --i)
			{
				var selected = selection[i];

				if (PrefabUtility.IsPartOfPrefabAsset(selected))
					continue;

				if (!selected.activeInHierarchy && onlyActiveObjects)
					continue;

				var prefabType = PrefabUtility.GetPrefabAssetType(prefab);
				GameObject newObject;

				if (prefabType == PrefabAssetType.Regular ||
					prefabType == PrefabAssetType.Model ||
					prefabType == PrefabAssetType.Variant)
				{
					newObject = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
				}
				else
				{
					newObject = Instantiate(prefab);
					newObject.name = prefab.name;
				}

				if (newObject == null)
				{
					Debug.LogError("Error instantiating prefab");
					break;
				}

				Undo.RegisterCreatedObjectUndo(newObject, "Replace With Prefabs");
				newObject.transform.parent = selected.transform.parent;

				if (!prefabPosition)
				{
					newObject.transform.localPosition = selected.transform.localPosition;
				}
				if (!prefabRotation)
				{
					newObject.transform.localRotation = selected.transform.localRotation;
				}
				if (!prefabScale)
				{
					newObject.transform.localScale = selected.transform.localScale;
				}

				newObject.transform.SetSiblingIndex(selected.transform.GetSiblingIndex());
				Undo.DestroyObjectImmediate(selected);
			}
		}

		oldPrefab = prefab;

		GUI.enabled = false;
		EditorGUILayout.LabelField("Selection count: " + Selection.objects.Length);

		if (previewTex != null)
		{
			//position = new Rect(100, 100, position.width, position.width + 100);

			const float gap = 20;
			float size = position.width - gap * 2;
			GUI.DrawTexture(new Rect(gap, 134, size, size), previewTex);
		}
	}
}