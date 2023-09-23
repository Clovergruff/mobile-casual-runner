using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIExt
{
	public static bool interactive = true;
	public static Canvas mainCanvas;

	public static Canvas GetParentCanvas(Transform trans)
	{
		return trans.root.GetComponentInChildren<Canvas>();
	}

	public static Vector2 WorldToCanvas(RectTransform rect, Transform target, RectTransform canvasRect, bool clampInView = false)
	{
		return WorldToCanvas(rect, target.position, canvasRect, clampInView);
	}

	public static Vector2 WorldToCanvas(RectTransform rect, Vector3 target, RectTransform canvasRect, bool clampInView = false)
	{
		Vector2 halfSize = rect.sizeDelta * 0.5f;
		Vector3 screenPoint = Camera.main.WorldToViewportPoint(target);

		if (screenPoint.z < 0)
		{
			return new Vector2(-canvasRect.sizeDelta.x * 100, -canvasRect.sizeDelta.y * 100);
		}
		else
		{
			float canvasY = canvasRect.sizeDelta.y;
			float canvasX = canvasRect.sizeDelta.x;

			Vector2 newPosition = new Vector2((screenPoint.x * canvasX) - (canvasX * 0.5f), (screenPoint.y * canvasY) - (canvasY * 0.5f));

			if (clampInView)
			{
				newPosition.x = Mathf.Clamp(newPosition.x, -canvasX * 0.5f + halfSize.x, canvasX * 0.5f - halfSize.x);
				newPosition.y = Mathf.Clamp(newPosition.y, -canvasY * 0.5f + halfSize.y, canvasY * 0.5f - halfSize.y);
			}

			//Mathf.Max(-canvasX * 0.5f + halfSize.x, Mathf.Min(canvasX * 0.5f - halfSize.x, ((screenPoint.x * canvasX) - (canvasX * 0.5f)))),
			//Mathf.Max(-canvasY * 0.5f + halfSize.y, Mathf.Min(canvasY * 0.5f - halfSize.y, ((screenPoint.y * canvasY) - (canvasY * 0.5f)))));

			return newPosition;
		}
	}
}