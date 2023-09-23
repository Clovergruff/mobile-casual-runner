using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorExt
{
	public static Color SetA(this Color color, float a)
	{
		color.a = a;
		return color;
	}

	public static Color Average(Color colorA, Color colorB)
	{
		float r = colorA.r + colorB.r;
		float g = colorA.g + colorB.g;
		float b = colorA.b + colorB.b;
		float a = colorA.a + colorB.a;

		const int Count = 2;
		return new Color(r / Count, g / Count, b / Count, a / Count);
	}

	public static Color Average(Color colorA, Color colorB, params Color[] restColors)
	{
		float r = colorA.r + colorB.r;
		float g = colorA.g + colorB.g;
		float b = colorA.b + colorB.b;
		float a = colorA.a + colorB.a;

		for (int i = 0; i < restColors.Length; i++)
		{
			r += restColors[i].r;
			g += restColors[i].g;
			b += restColors[i].b;
			a += restColors[i].a;
		}

		int count = 2 + restColors.Length;
		return new Color(r / count, g / count, b / count, a / count);
	}

	public static Color Average(IEnumerable<Color> colorEnumerator)
	{
		int count = 0;
		float r = 0f, g = 0f, b = 0f, a = 0f;

		foreach (Color color in colorEnumerator)
		{
			r += color.r;
			g += color.g;
			b += color.b;
			a += color.a;

			count++;
		}

		return new Color(r / count, g / count, b / count, a / count);
	}
}