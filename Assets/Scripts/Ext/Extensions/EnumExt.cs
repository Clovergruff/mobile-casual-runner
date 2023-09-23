using System;

public static class EnumExt
{
	/// <summary>
	/// Tries to convert value to TEnum, or returns defaultValue if value is not defined in TEnum
	/// </summary>
	public static TEnum ConvertOrValue<TEnum>(int value, TEnum defaultValue) where TEnum : struct
	{
		return Enum.IsDefined(typeof(TEnum), value) ? (TEnum)Enum.ToObject(typeof(TEnum), value) : defaultValue;
	}

	#region Enum EqualsAny
	public static bool EqualsAny(this Enum caller, Enum other1, Enum other2)
	{
		return caller.Equals(other1) || caller.Equals(other2);
	}
	public static bool EqualsAny(this Enum caller, Enum other1, Enum other2, Enum other3)
	{
		return caller.Equals(other1) || caller.Equals(other2) || caller.Equals(other3);
	}
	public static bool EqualsAny(this Enum caller, Enum other1, Enum other2, Enum other3, Enum other4)
	{
		return caller.Equals(other1) || caller.Equals(other2) || caller.Equals(other3) || caller.Equals(other4);
	}
	public static bool EqualsAny(this Enum caller, Enum other1, Enum other2, Enum other3, Enum other4, Enum other5)
	{
		return caller.Equals(other1) || caller.Equals(other2) || caller.Equals(other3) || caller.Equals(other4) || caller.Equals(other5);
	}
	public static bool EqualsAny(this Enum caller, Enum other1, Enum other2, Enum other3, Enum other4, Enum other5, Enum other6)
	{
		return caller.Equals(other1) || caller.Equals(other2) || caller.Equals(other3) || caller.Equals(other4) || caller.Equals(other5) || caller.Equals(other6);
	}

	public static bool EqualsAny(this Enum caller, Enum other1, Enum other2, Enum other3, Enum other4, Enum other5, Enum other6, Enum other7)
	{
		return caller.Equals(other1) || caller.Equals(other2) || caller.Equals(other3) || caller.Equals(other4) || caller.Equals(other5) || caller.Equals(other6) || caller.Equals(other7);
	}
	#endregion
}