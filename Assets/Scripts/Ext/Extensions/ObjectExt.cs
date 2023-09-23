using System;
using UnityEngine;
using Object = UnityEngine.Object;

public static class ObjectExt
{
	#region UnityObject EqualsAny
	public static bool EqualsAny(this Object caller, Object other1, Object other2)
	{
		return caller.Equals(other1) || caller.Equals(other2);
	}
	public static bool EqualsAny(this Object caller, Object other1, Object other2, Object other3)
	{
		return caller.Equals(other1) || caller.Equals(other2) || caller.Equals(other3);
	}
	public static bool EqualsAny(this Object caller, Object other1, Object other2, Object other3, Object other4)
	{
		return caller.Equals(other1) || caller.Equals(other2) || caller.Equals(other3) || caller.Equals(other4);
	}
	public static bool EqualsAny(this Object caller, Object other1, Object other2, Object other3, Object other4, Object other5)
	{
		return caller.Equals(other1) || caller.Equals(other2) || caller.Equals(other3) || caller.Equals(other4) || caller.Equals(other5);
	}
	public static bool EqualsAny(this Object caller, Object other1, Object other2, Object other3, Object other4, Object other5, Object other6)
	{
		return caller.Equals(other1) || caller.Equals(other2) || caller.Equals(other3) || caller.Equals(other4) || caller.Equals(other5) || caller.Equals(other6);
	}
	public static bool EqualsAny(this Object caller, Object other1, Object other2, Object other3, Object other4, Object other5, Object other6, Object other7)
	{
		return caller.Equals(other1) || caller.Equals(other2) || caller.Equals(other3) || caller.Equals(other4) || caller.Equals(other5) || caller.Equals(other6) || caller.Equals(other7);
	}
	#endregion

	#region Instantiate
	/// <summary>
	/// Creates instance of object Original, optionally keeping also the original name (removes (Clone) postfix).
	/// Use like this.Instantiate(prefab, keepName: true).
	/// </summary>
	public static T Instantiate<T>(this Object _, T original, bool keepName) where T : Object
	{
		T instanceOfOriginal = Object.Instantiate(original);
		if (keepName) instanceOfOriginal.name = original.name;
		return instanceOfOriginal;
	}
	/// <summary>
	/// Creates instance of object Original, optionally keeping also the original name (removes (Clone) postfix).
	/// Use like this.Instantiate(prefab, keepName: true).
	/// </summary>
	public static T Instantiate<T>(T original, bool keepName) where T : Object
	{
		T instanceOfOriginal = Object.Instantiate(original);
		if (keepName) instanceOfOriginal.name = original.name;
		return instanceOfOriginal;
	}
	#endregion

	#region UnityEngine.Object

	/// <summary>
	/// Unity does not support null-coalescing operator (??). This extension method solves that issue.
	/// Usage: mySprite.OrIfNull(myEmptySprite)
	/// </summary>
	public static T OrIfNull<T> (this T caller, T other) where T : Object
	{
		if (caller == null) return other;
		else return caller;
	}
	/// <summary>
	/// Unity's objects have fake nulls, which is overriden in == and != and works correctly.
	/// But in ?? and .? operators it can't be overriden, so it doesn't work how desired.
	/// You can use this method to solve that problem. mySprite.GetNull() ?? myEmptySprite.
	/// myText.GetNull().?update()
	/// </summary>
	public static T GetNulled<T> (this T caller) where T : Object
	{
		return caller ? caller : null;
	}

	#region Bulk Set Active

	public static void SetActive(bool value, GameObject a, GameObject b)
	{
		a.SetActive(value);
		b.SetActive(value);
	}
	public static void SetActive(bool value, GameObject a, GameObject b, GameObject c)
	{
		a.SetActive(value);
		b.SetActive(value);
		c.SetActive(value);
	}
	public static void SetActive(bool value, GameObject a, GameObject b, GameObject c, GameObject d)
	{
		a.SetActive(value);
		b.SetActive(value);
		c.SetActive(value);
		d.SetActive(value);
	}
	public static void SetActive(bool value, GameObject a, GameObject b, GameObject c, GameObject d, GameObject e)
	{
		a.SetActive(value);
		b.SetActive(value);
		c.SetActive(value);
		d.SetActive(value);
		e.SetActive(value);
	}
	public static void SetActive(bool value, GameObject a, GameObject b, GameObject c, GameObject d, GameObject e, GameObject f)
	{
		a.SetActive(value);
		b.SetActive(value);
		c.SetActive(value);
		d.SetActive(value);
		e.SetActive(value);
		f.SetActive(value);
	}
	public static void SetActive(bool value, GameObject a, GameObject b, GameObject c, GameObject d, GameObject e, GameObject f, GameObject g)
	{
		a.SetActive(value);
		b.SetActive(value);
		c.SetActive(value);
		d.SetActive(value);
		e.SetActive(value);
		f.SetActive(value);
		g.SetActive(value);
	}
	public static void SetActive(bool value, GameObject a, GameObject b, GameObject c, GameObject d, GameObject e, GameObject f, GameObject g, GameObject h)
	{
		a.SetActive(value);
		b.SetActive(value);
		c.SetActive(value);
		d.SetActive(value);
		e.SetActive(value);
		f.SetActive(value);
		g.SetActive(value);
		h.SetActive(value);
	}
	public static void SetActive(bool value, params GameObject[] objects)
	{
		for (int i = objects.Length - 1; i >= 0; i--)
		{
			objects[i].SetActive(value);
		}
	}

	#endregion

	#endregion
}