using System.Globalization;

public static class StringExt
{
    /// <summary>
    /// Returns string from number formatted to 1 234 567
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public static string ToStringSpaceThousands(this int number)
    {
        NumberFormatInfo nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
        nfi.NumberGroupSeparator = " ";
        return number.ToString("#,0", nfi); // "1 234 897"
    }
}