using UnityEngine;

public static class Curves
{
	public static readonly AnimationCurve straight = CreateStraight();
	public static readonly AnimationCurve linear = CreateLinear();
	public static readonly AnimationCurve smooth = CreateSmooth();
	public static readonly AnimationCurve easeIn = CreateEaseIn();
	public static readonly AnimationCurve easeOut = CreateEaseOut();
	public static readonly AnimationCurve wave = CreateWave();
	public static readonly AnimationCurve waveSharp = CreateWaveSharp();

	public static AnimationCurve CreateStraight(float height = 1)
	{
		AnimationCurve curve = new AnimationCurve();
		curve.AddKey(new Keyframe(0, height));
		curve.AddKey(new Keyframe(1, height));
		return curve;
	}

	public static AnimationCurve CreateLinear(float height = 1)
	{
		float tan45 = Mathf.Tan(Mathf.Deg2Rad * 45);

		AnimationCurve curve = new AnimationCurve();
		curve.AddKey(new Keyframe(0, 0, tan45, tan45));
		curve.AddKey(new Keyframe(1, height, tan45, tan45));
		return curve;
	}

	public static AnimationCurve CreateSmooth(float height = 1)
	{
		AnimationCurve curve = new AnimationCurve();
		curve.AddKey(new Keyframe(0, 0, 0, 0));
		curve.AddKey(new Keyframe(1, height, 0, 0));
		return curve;
	}

		public static AnimationCurve CreateSmoothInverse(float height = 1)
	{
		AnimationCurve curve = new AnimationCurve();
		curve.AddKey(new Keyframe(0, height, 0, 0));
		curve.AddKey(new Keyframe(1, 0, 0, 0));
		return curve;
	}

	public static AnimationCurve CreateEaseIn(float height = 1)
	{
		float tan45 = Mathf.Tan(Mathf.Deg2Rad * 45);
		AnimationCurve curve = new AnimationCurve();
		curve.AddKey(new Keyframe(0, 0, 0, 0));
		curve.AddKey(new Keyframe(1, height, tan45, tan45));
		return curve;
	}

	public static AnimationCurve CreateEaseOut(float height = 1)
	{
		float tan45 = Mathf.Tan(Mathf.Deg2Rad * 45);
		AnimationCurve curve = new AnimationCurve();
		curve.AddKey(new Keyframe(0, 0, tan45, tan45));
		curve.AddKey(new Keyframe(1, height, 0, 0));
		return curve;
	}

	public static AnimationCurve CreateWave(float height = 1)
	{
		AnimationCurve curve = new AnimationCurve();
		curve.AddKey(new Keyframe(0, 0, 0, 0));
		curve.AddKey(new Keyframe(0.5f, height, 0, 0));
		curve.AddKey(new Keyframe(1, 0, 0, 0));
		return curve;
	}

	public static AnimationCurve CreateWaveSharp(float height = 1)
	{
		float tan45 = Mathf.Tan(Mathf.Deg2Rad * 45);

		AnimationCurve curve = new AnimationCurve();
		curve.AddKey(new Keyframe(0, 0, tan45, tan45));
		curve.AddKey(new Keyframe(0.5f, height, 0, 0));
		curve.AddKey(new Keyframe(1, 0, -tan45, tan45));
		return curve;
	}

	public static void SetCurveLinear(AnimationCurve curve)
	{
		int curveKeyCount = curve.keys.Length;
		for (int i = 0; i < curveKeyCount; ++i)
		{
			float intangent = 0;
			float outtangent = 0;
			bool intangent_set = false;
			bool outtangent_set = false;

			Vector2 point1;
			Vector2 point2;
			Vector2 deltapoint;
			Keyframe key = curve[i];

			if (i == 0)
			{
				intangent = 0;
				intangent_set = true;
			}

			if (i == curveKeyCount - 1)
			{
				outtangent = 0;
				outtangent_set = true;
			}

			if (!intangent_set)
			{
				point1.x = curve.keys[i - 1].time;
				point1.y = curve.keys[i - 1].value;
				point2.x = curve.keys[i].time;
				point2.y = curve.keys[i].value;

				deltapoint = point2 - point1;

				intangent = deltapoint.y / deltapoint.x;
			}

			if (!outtangent_set)
			{
				point1.x = curve.keys[i].time;
				point1.y = curve.keys[i].value;
				point2.x = curve.keys[i + 1].time;
				point2.y = curve.keys[i + 1].value;

				deltapoint = point2 - point1;

				outtangent = deltapoint.y / deltapoint.x;
			}

			key.inTangent = intangent;
			key.outTangent = outtangent;
			curve.MoveKey(i, key);
		}
	}
}