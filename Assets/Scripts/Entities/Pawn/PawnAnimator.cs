using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnAnimator : MonoBehaviour
{
	public Animator animator;

	public PawnSkinInstance skinInstance {get; private set;}

	private Pawn pawn;

	public Vector3 rootPositionDelta			{ get; private set; }
	private Vector3 rootPositionOffset;

	public void Init(PawnSkinInstance skinInstance)
	{
		this.skinInstance = skinInstance;
		pawn = skinInstance.graphicsSystem.pawn;
	}

	private void Reset()
	{
		animator = GetComponent<Animator>();
	}

	private void FixedUpdate()
	{
		rootPositionDelta = rootPositionOffset; // * (1 / Time.fixedDeltaTime);//45;
		pawn.transform.position += rootPositionOffset;
		rootPositionOffset = Vector3.zero;
	}

	private void OnAnimatorMove()
	{
		rootPositionOffset += animator.deltaPosition;
	}

	public void FootstepLeft()
	{
		pawn.events.onFootstepLeft.Invoke();
	}

	public void FootstepRight()
	{
		pawn.events.onFootstepRight.Invoke();
	}
}
