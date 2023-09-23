using UnityEngine;

public static class VectorExt
{
	#region Setters
	public static Vector3 SetX(this Vector3 vector, float x)
	{
		vector.x = x;
		return vector;
	}
	public static Vector3 SetY(this Vector3 vector, float y)
	{
		vector.y = y;
		return vector;
	}
	public static Vector3 SetZ(this Vector3 vector, float z)
	{
		vector.z = z;
		return vector;
	}

	public static Vector2 SetX(this Vector2 vector, float x)
	{
		vector.x = x;
		return vector;
	}
	public static Vector2 SetY(this Vector2 vector, float y)
	{
		vector.y = y;
		return vector;
	}
	#endregion

	/// <summary>
	/// Returns max value in vector (x or y or z)
	/// </summary>
	public static float Max(this Vector3 vector) => Mathf.Max(Mathf.Max(vector.x, vector.y), vector.z);

	/// <summary>
	/// Same as Vector3.Lerp, but with t value for each axis
	/// </summary>
	public static Vector3 Lerp(this Vector3 a, Vector3 b, float tX, float tY, float tZ)
	{
		return new Vector3(
			a.x + (b.x - a.x) * Mathf.Clamp01(tX),
			a.y + (b.y - a.y) * Mathf.Clamp01(tY),
			a.z + (b.z - a.z) * Mathf.Clamp01(tZ)
		);
	}
	/// <summary>
	/// Same as Vector3.Lerp, but with t value for each axis
	/// </summary>
	public static Vector3 LerpUnclamped(this Vector3 a, Vector3 b, float tX, float tY, float tZ)
	{
		return new Vector3(
			a.x + (b.x - a.x) * tX,
			a.y + (b.y - a.y) * tY,
			a.z + (b.z - a.z) * tZ
		);
	}
	/// <summary>
	/// Same as Vector2.Lerp, but with t value for each axis
	/// </summary>
	public static Vector2 Lerp(this Vector2 a, Vector2 b, float tX, float tY)
	{
		return new Vector2(
			a.x + (b.x - a.x) * Mathf.Clamp01(tX),
			a.y + (b.y - a.y) * Mathf.Clamp01(tY)
		);
	}
	/// <summary>
	/// Same as Vector2.Lerp, but with t value for each axis
	/// </summary>
	public static Vector2 LerpUnclamped(this Vector2 a, Vector2 b, float tX, float tY)
	{
		return new Vector2(
			a.x + (b.x - a.x) * tX,
			a.y + (b.y - a.y) * tY
		);
	}

	#region NaN and Infinity

	public static readonly Vector3 NaN3 = new Vector3(float.NaN, float.NaN, float.NaN);
	public static readonly Vector2 NaN2 = new Vector2(float.NaN, float.NaN);

	public static bool IsAnyNaN(this Vector3 vector)
	{
		return float.IsNaN(vector.x) || float.IsNaN(vector.y) || float.IsNaN(vector.z);
	}

	public static bool IsAnyNaN(this Vector2 vector)
	{
		return float.IsNaN(vector.x) || float.IsNaN(vector.y);
	}

	public static bool IsAnyInfinite(this Vector3 vector)
	{
		return float.IsInfinity(vector.x) || float.IsInfinity(vector.y) || float.IsInfinity(vector.z);
	}

	public static bool IsAnyInfinite(this Vector2 vector)
	{
		return float.IsInfinity(vector.x) || float.IsInfinity(vector.y);
	}

	#endregion
}