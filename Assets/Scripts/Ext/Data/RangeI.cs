using System;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Struct for Range of Integers
/// </summary>
[System.Serializable]
public struct RangeI : IEquatable<RangeI>
{
	[FormerlySerializedAs("Min")]
	public int min;
	[FormerlySerializedAs("Max")]
	public int max;

	public RangeI(int min, int max)
	{
		this.min = min;
		this.max = max;
	}
	public RangeI(int[] array)
	{
		if (array == null) throw new ArgumentNullException(nameof(array));
		if (array.Length != 2) throw new ArgumentOutOfRangeException($"Length of {nameof(array)} is not 2");

		this.min = array[0];
		this.max = array[1];
	}

	public int GetRandom()
	{
		if (min == max) return min;
		else return UnityEngine.Random.Range(min, max + 1); // Max INCLUSIVE
	}
	public int[] ToArray() => new[] { min, max };

	/// <summary>
	/// Returns validated copy: Max is greater or equal to Min
	/// </summary>
	public RangeI ValidateNormal()
	{
		if (min <= max) return this;
		else return new RangeI(min: this.min, max: this.min);
	}
	public RangeI Clamp (int min, int max)
	{
		var copy = this;
		if (copy.min < min) copy.min = min;
		if (copy.max > max) copy.max = max;
		return copy;
	}

	public int GetDistance() => Mathf.Abs(max - min);

	public override string ToString() => $"({min}, {max})";

	#region Predefined values
	public static readonly RangeI Zero = new RangeI(0, 0);
    #endregion

    #region Equality
    public override bool Equals(object obj)
	{
		return obj is RangeI i && Equals(i);
	}
	public bool Equals(RangeI other)
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
	public static bool operator ==(RangeI left, RangeI right)
	{
		return left.Equals(right);
	}
	public static bool operator !=(RangeI left, RangeI right)
	{
		return !(left == right);
	}
	#endregion
}