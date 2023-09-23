using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExt
{
	/// <summary>
	/// Sets the layer to every child in game object, to given value
	/// </summary>
	public static void SetLayerRecursive(this GameObject gameObject, int layer)
	{
		if (gameObject == null)
		{
			return;
		}

		Transform transform = gameObject.transform;
		
		int childCount = transform.childCount;
		gameObject.layer = layer;
		
		for (int i = 0; i < childCount; i++)
		{
			Transform child = transform.GetChild(i);
			SetLayerRecursive(child.gameObject, layer);
		}
	}
}