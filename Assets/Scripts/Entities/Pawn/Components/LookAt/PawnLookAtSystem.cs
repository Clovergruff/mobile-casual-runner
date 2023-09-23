using UnityEngine;
using Gruffdev.BCS;
using BeauRoutine;
using System.Collections;

public class PawnLookAtSystem : PawnSystem<PawnLookAtConfig>
	, ILateUpdate
{
	public enum LookAtMode
	{
		Point,
		Transform,
	}

	[Range(0, 1)] public float influence = 1;
	public float adjustSpeed = 25;
	public Vector3 targetPoint;
	public Transform targetTransform;

	private Quaternion lookOffsetRotation = Quaternion.identity;

	private Routine _toggleRoutine = Routine.Null;
	private bool _enabled = true;
	private AnimationCurve _lookCurve = Curves.smooth;

	private delegate Vector3 GetTargetPositionMode();	
	GetTargetPositionMode getTargetPosition;

	public override void Init(Pawn pawn, PawnLookAtConfig config)
	{
		base.Init(pawn, config);

		getTargetPosition = GetTargetPointMode;
	}

	public void OnLateUpdate()
	{
		if (influence == 0)
			return;

		Vector3 target = getTargetPosition();

		var bones = pawn.graphics.skinInstance.bones;
		Vector3 lookDiff = target - pawn.graphics.skinInstance.bones.lookAimTransform.position;
		// float toTargetDot = Vector3.Dot(transform.forward, lookDiff.normalized);

		Quaternion lookRot = Quaternion.LookRotation(lookDiff);
		Quaternion aimForwardRot = Quaternion.LookRotation(pawn.graphics.skinInstance.bones.lookAimTransform.forward);

		float lookYawOffset = Mathf.DeltaAngle(aimForwardRot.eulerAngles.y, lookRot.eulerAngles.y) / bones.lookIKBones.Length;
		float lookPitchOffset = Mathf.DeltaAngle(aimForwardRot.eulerAngles.x, lookRot.eulerAngles.x) / bones.lookIKBones.Length;
		
		Quaternion YawRot = Quaternion.Euler(0, lookRot.eulerAngles.y, 0);
		Quaternion lookOffsetRotationTarget = YawRot * Quaternion.Euler(lookPitchOffset * 0.75f, 0, 0) * Quaternion.Euler(0, lookYawOffset, 0) * Quaternion.Inverse(YawRot);

		lookOffsetRotation = Quaternion.Slerp(lookOffsetRotation, lookOffsetRotationTarget, adjustSpeed * Time.deltaTime);

		foreach (var bone in bones.lookIKBones)
		{
			bone.transform.rotation = lookOffsetRotation * bone.transform.rotation;
		}
	}

	public void Enable()
	{
		if (_enabled)
			return;

		_enabled = true;
		_toggleRoutine.Stop();
		_toggleRoutine = Routine.Start(this, ToggleSequence(1));
	}

	public void Disable()
	{
		if (!_enabled)
			return;

		_enabled = false;
		_toggleRoutine.Stop();
		_toggleRoutine = Routine.Start(this, ToggleSequence(0));
	}

	private IEnumerator ToggleSequence(float targetInfluence)
	{
		float t = 0;
		float previousInfluence = influence;

		while (t < 1)
		{
			t += Time.deltaTime * 3;
			influence = Mathf.LerpUnclamped(previousInfluence, targetInfluence, _lookCurve.Evaluate(t));
			yield return null;
		}

		influence = Mathf.Clamp01(influence);
	}

	private Vector3 GetTargetPointMode() => targetPoint;
	private Vector3 GetTargetTransformPositionMode() => targetTransform.position;

	public void SetMode(LookAtMode newMode)
	{
		switch (newMode)
		{
			case LookAtMode.Point:
				getTargetPosition = GetTargetPointMode;
				break;
			case LookAtMode.Transform:
				getTargetPosition = GetTargetTransformPositionMode;
				break;
		}
	}
}