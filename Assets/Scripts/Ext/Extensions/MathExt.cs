using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimeScaleType
{
	Delta,
	UnscaledDelta,
	FixedDelta,
}

public enum RoundingType
{
    Default,
    Up,
    Down
}

public enum ScreenAxisType
{
	Width,
	Height,
	Both,
}

public static class MathExt
{
	private static readonly string[] SMALL_SUFFIX = { "", "u", "d", "t", "q", "Q", "s", "S", "o", "n" };
	private static readonly string[] BIG_SUFFIX = { "", "D", "V", "T", "q", "Q", "s", "S", "O", "N" };

	const float FULL_ANGLE = 360;
	const float STRAIGHT_ANGLE = 180;

	#region Basic
	public static bool IsOdd(int value) => value % 2 != 0;
	public static bool IsEven(int value) => value % 2 == 0;
	public static bool IsInteger(float value) => Mathf.Approximately(value, Mathf.RoundToInt(value));

	public static int BoolToInt(bool testBool) => testBool ? 1 : 0;
	public static bool IntToBool(int testInt) => testInt > 0;

	public static bool IsApproximatelyEqual(float a, float b, float tolerance = 0.001f) => Mathf.Abs(a - b) < tolerance;

	public static int GetDigitFromInteger(int x, int n)
	{
		while (n-- > 0)
			x /= 10;
		return (x % 10);
	}
#endregion

	#region Interpolation
	public static Vector3 LerpBezier(Vector3 s, Vector3 e, Vector3 p, float t)
	{
		var rt = 1 - t;
		return rt * rt * s + 2 * rt * t * p + t * t * e;
	}

	public static Vector3 LerpMultiple(Vector3[] vectorPoints, float t)
	{
		t = Mathf.Clamp01(t);
		if (vectorPoints == null || vectorPoints.Length == 0)
			throw (new System.Exception("Vectors input must have at least one value"));

		if (vectorPoints.Length == 1)
			return vectorPoints[0];

		if (t == 0)
			return vectorPoints[0];

		if (t == 1)
			return vectorPoints[vectorPoints.Length - 1];

		float t2 = t * vectorPoints.Length;
		int p = (int)Mathf.Floor(t2);
		t2 -= p;

		return Vector3.Lerp(vectorPoints[p], vectorPoints[Mathf.Min(p + 1, vectorPoints.Length-1)], t2);
	}

	public static Vector2 PixelToScreenPercentage(Vector2 vector, ScreenAxisType axis = ScreenAxisType.Both)
	{
		float testX = Screen.width;
		float testY = Screen.height;

		if (axis == ScreenAxisType.Width)
			testY = testX;
		else if (axis == ScreenAxisType.Height)
			testX = testY;

		float x = InverseLerpUnclamped(0, testX, vector.x);
		float y = InverseLerpUnclamped(0, testY, vector.y);

		return new Vector2(x, y);
	}

	/// <summary>
	/// Same as Mathf.InverseLerp, but without clamping to 01
	/// </summary>
	public static float InverseLerpUnclamped(float min, float max, float value)
	{
		return Mathf.Abs(max - min) < Mathf.Epsilon ? min : (value - min) / (max - min);
	}

	public static float InverseLerpUnclamped(Vector3 a, Vector3 b, Vector3 value)
	{
		Vector3 AB = b - a;
		Vector3 AV = value - a;
		return Vector3.Dot(AV, AB) / Vector3.Dot(AB, AB);
	}

	public static float InverseLerp(Vector3 a, Vector3 b, Vector3 value)
	{
		return Mathf.Clamp01(InverseLerpUnclamped(a, b, value));
	}

	public static float LerpAngle(float a, float b, float t)
	{
		while (a > b + STRAIGHT_ANGLE)
			b += FULL_ANGLE;

		while (b > a + STRAIGHT_ANGLE)
			b -= FULL_ANGLE;

		return Mathf.Lerp(a, b, t);
	}
	#endregion

	#region Angles/Rotation
	public static float Vector2AngleInRad(Vector2 vec1, Vector2 vec2) => Mathf.Atan2(vec2.y - vec1.y, vec2.x - vec1.x);
	public static float Vector2AngleInRad(Vector2 vec) => Mathf.Atan2(vec.y, vec.x);
	public static float Vector2AngleInDeg(Vector2 vec1, Vector2 vec2) => Vector2AngleInRad(vec1, vec2) * 180 / Mathf.PI;
	public static float Vector2AngleInDeg(Vector2 vec) => Vector2AngleInRad(vec) * 180 / Mathf.PI;

	public static Vector2 Rotate(this Vector2 v, float degrees)
	{
		var radians = degrees * Mathf.Deg2Rad;
		var sin = Mathf.Sin(radians);
		var cos = Mathf.Cos(radians);

		var tx = v.x;
		var ty = v.y;

		return new Vector2(cos * tx - sin * ty, sin * tx + cos * ty);
	}

	public static Vector2 RotateVector2(Vector2 v, float degrees) => v.Rotate(degrees);
	public static Vector2 RadianToVector2(float radian) => new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
	public static Vector2 DegreeToVector2(float degree) => RadianToVector2(degree * Mathf.Deg2Rad);

	public static float ClampAngle(float angle, float from, float to)
	{
		if (angle > 180) angle = 360 - angle;
		angle = Mathf.Clamp(angle, from, to);
		if (angle < 0) angle = 360 + angle;

		return angle;
	}

	#endregion

	#region Spatial calculations
		public static float LineDistance(Vector3 pointPos, Vector3 edge1Pos, Vector3 edge2Pos)
	{
		var pA = pointPos.x - edge1Pos.x;
		var pB = pointPos.y - edge1Pos.y;
		var pC = pointPos.z - edge1Pos.z;

		var eA = edge2Pos.x - edge1Pos.x;
		var eB = edge2Pos.y - edge1Pos.y;
		var eC = edge2Pos.z - edge1Pos.z;
	
		var dot = pA*eA + pB*eB + pC*eC;
		var lenSq = eA*eA + eB*eB + eC*eC;

		float param = -1;
		if (lenSq != 0) // In case of 0 length line
		{
			param = dot / lenSq;
		}

		float xx, yy, zz;

		if (param < 0)
		{
			xx = edge1Pos.x;
			yy = edge1Pos.y;
			zz = edge1Pos.z;
		}
		else if (param > 1)
		{
			xx = edge2Pos.x;
			yy = edge2Pos.y;
			zz = edge2Pos.z;
		}
		else
		{
			xx = edge1Pos.x + param * eA;
			yy = edge1Pos.y + param * eB;
			zz = edge1Pos.z + param * eC;
		}

		var dx = pointPos.x - xx;
		var dy = pointPos.y - yy;
		var dz = pointPos.z - zz;
		return Mathf.Sqrt(dx*dx + dy*dy + dz*dz);
	}

	public static Vector2 NearestInfiniteLinePoint(Vector3 linePosition, Vector3 lineDirection, Vector3 position)
	{
		lineDirection.Normalize();
		Vector3 lhs = position - linePosition;

		float dotP = Vector3.Dot(lhs, lineDirection);
		return linePosition + lineDirection * dotP;
	}

	public static Vector3 NearestLinePoint(Vector3 point1, Vector3 point2, Vector3 position)
	{
		// Get heading
		Vector3 heading = (point2 - point1);
		float magnitudeMax = heading.magnitude;
		heading.Normalize();

		// Do projection from the point but clamp it
		Vector3 lhs = position - point1;
		float dotP = Vector3.Dot(lhs, heading);
		dotP = Mathf.Clamp(dotP, 0f, magnitudeMax);
		return point1 + heading * dotP;
	}

	public static Vector2 AverageVector(Vector2[] positions)
	{
		if (positions.Length == 0)
			return Vector2.zero;

		int count = positions.Length;
		float x = 0, y = 0;

		foreach (Vector2 pos in positions)
		{
			x += pos.x;
			y += pos.y;
		}
		return new Vector2(x / count, y / count);
	}

	public static Vector3 AverageVector(Vector3[] positions)
	{
		if (positions.Length == 0)
			return Vector3.zero;

		int count = positions.Length;
		float x = 0, y = 0, z = 0;

		foreach (Vector3 pos in positions)
		{
			x += pos.x;
			y += pos.y;
			z += pos.z;
		}
		return new Vector3(x / count, y / count, z / count);
	}

	public static Vector3 AveragePosition(Transform[] transforms)
	{
		if (transforms.Length == 0)
			return Vector3.zero;

		int count = transforms.Length;
		float x = 0, y = 0, z = 0;

		foreach (Transform trans in transforms)
		{
			x += trans.position.x;
			y += trans.position.y;
			z += trans.position.z;
		}
		return new Vector3(x / count, y / count, z / count);
	}

	#endregion

	#region Intersections, Overlaps
	public static bool IsInsideFrustum(Vector3 pos, Vector3 boundSize, Camera camera)
	{
		var planes = GeometryUtility.CalculateFrustumPlanes(camera);
		return IsInsideFrustum(pos, boundSize, planes);
	}

	public static bool IsInsideFrustum(Vector3 pos, Vector3 boundSize, Plane[] planes)
	{
		var bounds = new Bounds(pos, boundSize);
		return GeometryUtility.TestPlanesAABB(planes, bounds);
	}

	public static (int solution, Vector2 intersection1, Vector2 intersection2) GetCircleLineIntersections(
		Vector2 center, float radius,
		Vector2 point1, Vector2 point2,
		bool infinite)
	{
		float dx, dy, A, B, C, det, t;
		Vector2 intersection1 = Vector2.zero;
		Vector2 intersection2 = Vector2.zero;
		int result = 0;

		dx = point2.x - point1.x;
		dy = point2.y - point1.y;

		A = dx * dx + dy * dy;
		B = 2 * (dx * (point1.x - center.x) + dy * (point1.y - center.y));
		C = (point1.x - center.x) * (point1.x - center.x) +
			(point1.y - center.y) * (point1.y - center.y) -
			radius * radius;

		det = B * B - 4 * A * C;
		if (A <= 0.0000001 || det < 0)
		{
			// No real solutions.
		}
		else if (det == 0)
		{
			// One solution.
			t = -B / (2 * A);
			intersection1 =
				new Vector2(point1.x + t * dx, point1.y + t * dy);
			intersection2 = new Vector2(0, 0);
			result = 1;
		}
		else
		{
			// Two solutions.
			t = (float)((-B + Mathf.Sqrt(det)) / (2 * A));
			intersection1 =
				new Vector2(point1.x + t * dx, point1.y + t * dy);
			t = (float)((-B - Mathf.Sqrt(det)) / (2 * A));
			intersection2 =
				new Vector2(point1.x + t * dx, point1.y + t * dy);
			result = 2;
		}

		if (result != 0 && !infinite)
		{
			Vector2 centerPoint = Vector2.Lerp(point1, point2, 0.5f);
			float lineHalf = Vector2.Distance(point1, point2) * 0.5f;
			float dist1 = Vector2.Distance(centerPoint, intersection1);
			float dist2 = Vector2.Distance(centerPoint, intersection2);
			float nearestDist = dist1 < lineHalf ? dist1 : dist2;

			if (nearestDist > lineHalf)
				result = 0;
		}

		return (result, intersection1, intersection2);
	}

	#endregion

	#region Text Formatting
	public static string FloatToTime(float timer)
	{
		int minutes = Mathf.FloorToInt(timer / 60F);
		int seconds = Mathf.FloorToInt(timer - minutes * 60);
		return string.Format("{0:00} : {1:00}", minutes, seconds);
	}

	public static string GetLastIntegerSuffix(int num)
	{
		var intString = num.ToString();
		var stringLen = intString.Length;

		var lastChar = intString[stringLen - 1];
		var prevChar = ' ';
		if (stringLen > 1)
			prevChar = intString[stringLen - 2];
				

		var suffix = "th";

		switch (lastChar)
		{
			case '0':
			break;
			case '1':
				if (prevChar != '1')
					suffix = "st";
			break;
			case '2':
				if (prevChar != '1')
					suffix = "nd";
			break;
			case '3':
				if (prevChar != '1')
					suffix = "rd";
			break;
		}

		return suffix;
	}

    public static string GetNumberWithSuffix(float num, int maxPrecision = 1, bool useSpace = false, float suffixSize = -1, bool smallSuffix = false, RoundingType rounding = RoundingType.Default)
	{
        return GetNumberWithSuffix((double)num, maxPrecision, useSpace, suffixSize, smallSuffix, rounding);
	}
    public static string GetNumberWithSuffix(double num, int maxPrecision = 1, bool useSpace = false, float suffixSize = -1, bool smallSuffix = false, RoundingType rounding = RoundingType.Default)
    {
        var digitCount = (int)Mathf.Max(0, (float)System.Math.Floor(System.Math.Floor(System.Math.Log10(num))) / 3);
        var suffix = "";
        switch (digitCount)
        {
            case 0:
                break;
            case 1:
                suffix = "K";
                break;
            case 2:
                suffix = "M";
                break;
            case 3:
                suffix = "B";
                break;
            case 100:
                suffix = "C";
                break;
            case 101:
                suffix = "uC";
                break;
            case 102:
                suffix = "dC";
                break;
            default:
                if ((digitCount - 1) / 10 == 0)
                    suffix = SMALL_SUFFIX[(digitCount - 1) % 10];
                else
                {
                    try
                    {
                        suffix = SMALL_SUFFIX[(digitCount - 1) % 10] + BIG_SUFFIX[(digitCount - 1) / 10];
                    }
                    catch
                    {
                    }
                }
                break;
        }

        if (suffix != "")
        {
            if (smallSuffix)
                suffix.ToLower();
			else
                suffix.ToUpper();

			if (suffixSize != -1)
                suffix = "<size=" + suffixSize.ToString() + ">" + suffix + "</size>";

            if (useSpace)
                suffix = " " + suffix;

        }
        double finalNum = System.Math.Max(0, System.Math.Round(num / System.Math.Pow(10, digitCount * 3), 3));
        if (finalNum.ToString().Contains("Infinity"))
            finalNum = 0;
        if (num > 100)
        {
            if (finalNum < 10)
            {
                if (rounding == RoundingType.Down)
                {
                    finalNum = Mathf.FloorToInt((float)finalNum * Mathf.Pow(10,maxPrecision)) / Mathf.Pow(10, maxPrecision);
                }
                if (rounding == RoundingType.Up)
                {
                    finalNum = Mathf.CeilToInt((float)finalNum * Mathf.Pow(10, maxPrecision)) / Mathf.Pow(10, maxPrecision);
                }
                return finalNum.ToString("F" + maxPrecision.ToString()) + suffix;
            }
            if (finalNum < 100)
            {
                if(rounding == RoundingType.Down)
                {
                    finalNum = Mathf.FloorToInt((float)finalNum * 10f) / 10f;
                }
                if(rounding == RoundingType.Up)
                {
                    finalNum = Mathf.CeilToInt((float)finalNum * 10f) / 10f;
                }
                return finalNum.ToString("F1") + suffix;
            }
        }

        if (rounding == RoundingType.Down)
        {
            finalNum = Mathf.FloorToInt((float)finalNum);
        }
        if (rounding == RoundingType.Up)
        {
            finalNum = Mathf.CeilToInt((float)finalNum);
        }
        return finalNum.ToString("F0") + suffix;
    }

	public static string GetMetricNumber(double num, int numbersAfterComa = 1, int spacesAfterNumber = 0)
	{
		var format = "F" + numbersAfterComa;
		var suffix = "m";
		var numString = num.ToString("F0");
		if (num >= 1000)
		{
			numString = (num / 1000f).ToString(format);
			suffix = "km";
		}

		string space = "";
		if (spacesAfterNumber > 0)
		{
			for (var i = 0; i < spacesAfterNumber; i++)
				space += ' ';
		}
			
			
		return numString + space + suffix;
	}
}
#endregion