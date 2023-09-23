using System;
using UnityEngine;

/// <summary>
/// Struct for Range of Floats
/// </summary>
[System.Serializable]
public struct RangeF : IEquatable<RangeF>
{
    public float min;
	public float max;

	public RangeF (float min, float max)
	{
		this.min = min;
		this.max = max;
	}
	public RangeF(float[] array)
	{
		if (array == null) throw new ArgumentNullException(nameof(array));
		if (array.Length != 2) throw new ArgumentOutOfRangeException($"Length of {nameof(array)} is not 2");

		this.min = array[0];
		this.max = array[1];
	}

	public float GetRandom()
	{
		if (min == max) return min;
		else return UnityEngine.Random.Range(min, max);
	}
	/// <summary>
	/// Returns validated copy: Max is greater or equals to Min
	/// </summary>
	public RangeF ValidateNormal()
	{
		if (max >= min) return this;
		else return new RangeF(min: this.max, max: this.min); // swap values
	}
	public float[] ToArray() => new[] { min, max };

	public RangeF Clamp(float min, float max)
	{
		var copy = this;
		if (copy.min < min) copy.min = min;
		else if (copy.min > max) copy.min = max;
		if (copy.max < min) copy.max = min;
		else if (copy.max > max) copy.max = max;
		return copy;
	}

	public float GetDistance() => Mathf.Abs(max - min);

	public override string ToString() => $"({min}, {max})";

	#region Equality
	public override bool Equals(object obj)
	{
		return obj is RangeF f && Equals(f);
	}
	public bool Equals(RangeF other)
	{
		return min == other.min && max == other.max;
	}
	public override int GetHashCode()
	{
		var hashCode = 1537547080;
		hashCode = hashCode * -1521134295 + min.GetHashCode();
		hashCode = hashCode * -1521134295 + max.GetHashCode();
		return hashCode;
	}
	#endregion
}