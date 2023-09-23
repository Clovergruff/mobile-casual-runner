using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class TransformExt
{
	/// <summary>
	/// Looks in each parent for a component of type T
	/// </summary>
	public static T FindUpOfType<T>(this Transform transform, bool includeSelf = false)
	{
		Transform checkTr = includeSelf ? transform : transform.parent;
		while (checkTr != null)
		{
			T comp = checkTr.GetComponent<T>();
			if (comp != null) return comp;

			checkTr = checkTr.parent;
		}
		return default;
	}

	/// <summary>
	/// The same as GetComponentsInChildren, but caller (root) is not checked for component. If parentToIgnore is not null, every children of it will be ignored.
	/// </summary>
	public static List<T> GetComponentsInChildrenIgnoring<T>(this Transform rootTr, Transform parentToIgnore) where T : Component
	{
		List<T> result = new List<T>();
		GetComponentsInChildrenIgnoring(rootTr, parentToIgnore, result);
		return result;
	}
	/// <summary>
	/// The same as GetComponentsInChildren, but caller (root) is not checked for component. If parentToIgnore is not null, every children of it will be ignored.
	/// </summary>
	public static void GetComponentsInChildrenIgnoring<T>(this Transform rootTr, Transform parentToIgnore, List<T> result) where T : Component
	{
		GetChildComponentsRecursive(rootTr, parentToIgnore, result);
	}

	private static void GetChildComponentsRecursive<T>(Transform parent, Transform ignore, List<T> components) where T : Component
	{
		if (parent == null || parent.gameObject == null) return;
		if (parent == ignore && ignore != null) return;

		if (parent.gameObject.layer != 1)
		{
			T comp = parent.gameObject.GetComponent<T>();
			if (comp != null) components.Add(comp);
		}

		int childCount = parent.childCount;
		for (int i = 0; i < childCount; i++)
		{
			Transform child = parent.GetChild(i);
			if (child != null && child.gameObject != null && child.gameObject.layer != 1)
			{
				T comp = child.gameObject.GetComponent<T>();
				if (comp != null) components.Add(comp);
			}

			GetChildComponentsRecursive(child, ignore, components);
		}
	}

	/// <summary>
	/// Find Transform with name 'name'. Looks for childs of childs too (deep search).
	/// Breadth-First search.
	/// Ref: https://answers.unity.com/questions/799429/transformfindstring-no-longer-finds-grandchild.html
	/// </summary>
	public static Transform FindDeepBreadthFirst(this Transform parent, string name)
	{
		Queue<Transform> queue = new Queue<Transform>();
		queue.Enqueue(parent);
		while (queue.Count > 0)
		{
			var c = queue.Dequeue();
			if (c.name == name)
				return c;

			int childCount = c.childCount;
			for (int i = 0; i < childCount; i++)
				queue.Enqueue(c.GetChild(i));
		}
		return null;
	}

	/// <summary>
	/// Find Transform with name 'name'. Looks for childs of childs too (deep search).
	/// Depth-First search.
	/// Ref: https://answers.unity.com/questions/799429/transformfindstring-no-longer-finds-grandchild.html
	/// </summary>
	public static Transform FindDeepDepthFirst(this Transform parent, string name)
	{
		foreach (Transform child in parent)
		{
			if (child.name == name)
				return child;
			var result = child.FindDeepDepthFirst(name);
			if (result != null)
				return result;
		}
		return null;
	}

	/// <summary>
	/// Destroys all children game objects
	/// </summary>
	/// <param name="transform"></param>
	public static void DestroyChildren(this Transform transform)
	{
		for (int i = transform.childCount - 1; i >= 0; i--)
		{
			Object.Destroy(transform.GetChild(i).gameObject);
		}
	}

	/// <summary>
	/// Transforms a point, ignoring object's scale. See <see href="https://answers.unity.com/questions/1238142/version-of-transformtransformpoint-which-is-unaffe.html"/>.
	/// </summary>    /// 
	/// <param name="transform">Transform component to use for the operation.</param>
	/// <param name="position">Point to transform.</param>
	/// <returns>
	/// A Vector3 representing a transformed position, unaffected by object's scale.
	/// </returns>
	public static Vector3 TransformPointUnscaled(this Transform transform, Vector3 position)
	{
		var localToWorldMatrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
		return localToWorldMatrix.MultiplyPoint3x4(position);
	}

	/// <summary>
	/// Inverse-transforms a point, ignoring object's scale. See <see href="https://answers.unity.com/questions/1238142/version-of-transformtransformpoint-which-is-unaffe.html"/>.
	/// </summary>
	/// <param name="transform">Transform component to use for the operation.</param>
	/// <param name="position">Point to transform.</param>
	/// <returns>
	/// A Vector3 representing an inversed transformed position, unaffected by object's scale.
	/// </returns>
	public static Vector3 InverseTransformPointUnscaled(this Transform transform, Vector3 position)
	{
		var worldToLocalMatrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one).inverse;
		return worldToLocalMatrix.MultiplyPoint3x4(position);
	}

	/// <summary>
	/// Returns full hierarchy path to the transform. One/Two/Three/MyTransform
	/// </summary>
	public static string GetHierarchyPath(this Transform transform)
	{
		List<string> path = new List<string>();

		Transform parent = transform;
		do
		{
			path.Add(parent.name);
			parent = parent.parent;
		}
		while (parent != null);

		return string.Join("/", path.AsEnumerable().Reverse());
	}
}