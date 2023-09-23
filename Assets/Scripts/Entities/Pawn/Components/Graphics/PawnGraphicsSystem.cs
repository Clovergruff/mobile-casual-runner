using UnityEngine;
using Gruffdev.BCS;
using static UnityEngine.ParticleSystem;
using System;


[AddComponentMenu("Pawn/Graphics")]
public class PawnGraphicsSystem : PawnSystem<PawnGraphicsConfig>
	, IUpdate
	, IFixedUpdate
{
	public PawnSkinInstance skinInstance {get; private set;}
	public PawnSkinData skinData {get; private set;}

	public float lean;
	public float leanTarget;

	private Vector3 _directionSmooth;

	private Vector3 _preFixedPosition;
	private Quaternion _preFixedRotation = Quaternion.identity;
	private Quaternion _localPivotRot = Quaternion.identity;

	private Vector3 _adjustedInterpolationPosition;

	private delegate void GraphicsUpdate();
	private GraphicsUpdate _graphicsUpdate;

	public override void Init(Pawn pawn, PawnGraphicsConfig config)
	{
		base.Init(pawn, config);
	}

	public override void LateSetup()
	{
		ApplySkin(config.skinData);

		_preFixedPosition = transform.position;
		_preFixedRotation = transform.rotation;

		_graphicsUpdate = NormalGraphicsUpdate;

		pawn.events.onDeath += OnDeath;

		pawn.events.onStartMoving += OnStartMoving;
		pawn.events.onStopMoving += OnStopMoving;
		pawn.events.onGrounded += OnGrounded;
		pawn.events.onUngrounded += OnUngrounded;

		pawn.events.onFootstepLeft += OnFootstepLeft;
		pawn.events.onFootstepRight += OnFootstepRight;

		pawn.events.onJump += OnJump;
	}

	public void OnUpdate()
	{
		_graphicsUpdate();
	}

	public void OnFixedUpdate()
	{
		_preFixedPosition = transform.position;
		_preFixedRotation = transform.rotation;

		if (pawn.hasRotator)
		{
			var localVelocity = pawn.physics.localVelocity + skinInstance.pawnAnimator.rootPositionDelta * 60;
			leanTarget = Mathf.Clamp(pawn.rotator.behaviour.turnDelta * localVelocity.z * 0.1f * config.turnLeanMultiplier, -config.maxLeanAngle, config.maxLeanAngle);
		}
	}

	private void NoGraphicsUpdate() { }

	private void NormalGraphicsUpdate()
	{
		float alpha = (Time.time - Time.fixedTime) / Time.fixedDeltaTime;

		lean = Mathf.LerpAngle(lean, leanTarget, 20 * Time.deltaTime);
		_localPivotRot = Quaternion.Lerp(_localPivotRot, Quaternion.Euler(0, 0, lean), 20 * Time.deltaTime);

		Vector3 interpolatedPosition = Vector3.Lerp(_preFixedPosition, transform.position, alpha);
		Quaternion interpolatedRotation = Quaternion.Lerp(_preFixedRotation, transform.rotation, alpha);

		_adjustedInterpolationPosition.x = interpolatedPosition.x;
		_adjustedInterpolationPosition.z = interpolatedPosition.z;
		_adjustedInterpolationPosition.y = Mathf.Lerp(_adjustedInterpolationPosition.y, interpolatedPosition.y, 20 * Time.deltaTime);

		_directionSmooth = Vector3.Lerp(_directionSmooth, pawn.physics.localVelocityPercentage, 20 * Time.deltaTime);

		var animator = skinInstance.pawnAnimator.animator;
		animator.SetFloat(AnimHash.LOCAL_SPEED_X, _directionSmooth.x);
		animator.SetFloat(AnimHash.LOCAL_SPEED_Z, _directionSmooth.z);

		skinInstance.bones.centerPivot.localRotation = _localPivotRot;
		skinInstance.transform.position = _adjustedInterpolationPosition;
		skinInstance.transform.rotation = interpolatedRotation;
	}

	public void ApplySkin(PawnSkinData skinData)
	{
		ClearSkin();

		this.skinData = skinData;

		skinInstance = Instantiate(skinData.prefab, transform.position, transform.rotation, transform);
		skinInstance.transform.localScale = Vector3.one * config.scale;
		skinInstance.Init(this);

		skinInstance.collider.transform.SetParent(transform);

		pawn.events.onSkinApplied.Invoke(skinInstance);
	}

	public void ClearSkin()
	{
		if (skinInstance)
		{
			Destroy(skinInstance.collider.gameObject);
			Destroy(skinInstance.gameObject);
		}

		skinData = null;
	}

	private void OnDeath(Pawn pawn)
	{
		skinInstance.collider.gameObject.SetActive(false);
		var gibExplosionOrigin = skinInstance.bones.bodyCenter.position;
		var gibPhysicMaterial = CommonAssetsData.I.physicMaterials.gibs;

		foreach (var sphereoid in skinInstance.sphereoids)
		{
			sphereoid.EnableGibPhysics(gibExplosionOrigin, gibPhysicMaterial);
		}

		var deathEffect = skinInstance.effects.death;
		deathEffect.transform.position = skinInstance.bones.chest.position;
		deathEffect.Play(true);
	}

	public void OnStartMoving()
	{
		skinInstance.pawnAnimator.animator.SetBool(AnimHash.MOVING, true);
		skinInstance.pawnAnimator.animator.SetTrigger(AnimHash.MOVING_START);
	}
	public void OnStopMoving()
	{
		skinInstance.pawnAnimator.animator.SetBool(AnimHash.MOVING, false);
		skinInstance.pawnAnimator.animator.SetTrigger(AnimHash.MOVING_END);
	}

	public void OnGrounded(Vector3 velocity)
	{
		skinInstance.pawnAnimator.animator.SetBool(AnimHash.GROUNDED, true);

		if (velocity.y < -0.5)
		{
			if (velocity.y > -20)
			{
				if (pawn.physics.isMoving)
					skinInstance.pawnAnimator.animator.SetInteger(AnimHash.LAND_ID, 3);
				else
					skinInstance.pawnAnimator.animator.SetInteger(AnimHash.LAND_ID, 1);
			}
			else
			{
				skinInstance.pawnAnimator.animator.SetInteger(AnimHash.LAND_ID, 2);
			}

			skinInstance.pawnAnimator.animator.SetTrigger(AnimHash.LAND);
		}
	}
	public void OnUngrounded() => skinInstance.pawnAnimator.animator.SetBool(AnimHash.GROUNDED, false);

	private void OnJump()
	{
		int id = 0;

		if (pawn.physics.groundVelocity.magnitude < 1.5f)
		{
			id = 2;
		}
		else
		{
			id = 3;
		}

		skinInstance.pawnAnimator.animator.SetInteger(AnimHash.JUMP_ID, id);
		skinInstance.pawnAnimator.animator.SetTrigger(AnimHash.JUMP);
	}

	private void OnFootstepRight()
	{
		var effect = skinInstance.effects.footstepRight;
		effect.transform.position = skinInstance.bones.footstepPointRight.position;
		effect.Play();
	}

	private void OnFootstepLeft()
	{
		var effect = skinInstance.effects.footstepLeft;
		effect.transform.position = skinInstance.bones.footstepPointLeft.position;
		effect.Play();
	}
}
