using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnSkinInstance : MonoBehaviour
{
	public PawnAnimator pawnAnimator;
	public PawnAudiobox audioBox;
	public PawnEffects effects;
	public new PawnCollider collider;

	public PawnBodySphereoid[] sphereoids;
	public Bones bones;

	public PawnGraphicsSystem graphicsSystem	{get; private set;}
	public Pawn pawn							{get; private set;}

	public virtual void Init(PawnGraphicsSystem graphicsSystem)
	{
		this.graphicsSystem = graphicsSystem;
		pawn = graphicsSystem.pawn;

		pawnAnimator.Init(this);
	}

	public Transform GetCameraFollowTarget()
	{
		return bones.cameraFollowTarget;
	}

	[System.Serializable]
	public class Bones
	{
		public Transform centerPivot;
		public Transform cameraFollowTarget;

		[Header("Look At")]
		public Transform lookAimTransform;
		public Transform[] lookIKBones;

		[Header("Main bones")]
		public Transform bodyCenter;
		public Transform chest;
		public Transform head;

		[Header("Feet")]
		public Transform footstepPointLeft;
		public Transform footstepPointRight;
	}
}
