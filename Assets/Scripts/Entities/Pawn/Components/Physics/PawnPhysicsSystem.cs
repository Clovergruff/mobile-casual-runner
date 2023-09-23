using UnityEngine;
using Gruffdev.BCS;
using System;


[AddComponentMenu("Pawn/Physics")]
public class PawnPhysicsSystem : PawnSystem<PawnPhysicsConfig>
	, IUpdate
	, IFixedUpdate
{
	private Vector3 _localLocomotionVector;
	private Vector3 _globalLocomotionVector;

	public Vector3 groundVelocity = new Vector3();

	public bool isMoving {get; private set;}

	public PawnPhysicsState currentPhysicsState					{ get; private set; }
	public PawnGroundedState groundedState						{ get; private set; } = new PawnGroundedState();
	public PawnFallingState fallingState						{ get; private set; } = new PawnFallingState();
	public PawnFlyingState flyingState							{ get; private set; } = new PawnFlyingState();

	public Vector3 localVelocity								{ get; private set; }
	public Vector3 localVelocityPercentage						{ get; private set; }
	public Vector3 interpolatedLocalVelocity					{ get; private set; }
	public Vector3 interpolatedGroundVelocity					{ get; private set; }

	private Vector3 _preFixedLocalVelocity;
	private Vector3 _preFixedGroundVelocity;

	public PawnPhysicsConfig.PawnPhysicsDataProfile currentValues { get; private set; }

	public override void LateSetup()
	{
		groundedState.Setup(this);
		fallingState.Setup(this);
		flyingState.Setup(this);

		currentValues = config.midAirValues;
		currentPhysicsState = fallingState;

		pawn.events.onGrounded += OnGrounded;
		pawn.events.onUngrounded += OnUngrounded;
	}

	private void OnUngrounded() => SetPhysicsState(fallingState);
	private void OnGrounded(Vector3 previousVelocity) => SetPhysicsState(groundedState);

	public void SetPhysicsState(PawnPhysicsState newState)
	{
		currentPhysicsState.OnDisable();
		currentPhysicsState = newState;
		currentPhysicsState.OnEnable();
	}

	public void OnFixedUpdate()
	{
		currentPhysicsState.FixedUpdate();

		currentValues = pawn.hasGroundDetector && pawn.groundDetector.isGrounded
			? config.groundedValues
			: config.midAirValues;

		_preFixedGroundVelocity = groundVelocity;

		Vector3 newVel = pawn.body.rigidbody.velocity;
		groundVelocity.x = newVel.x;
		groundVelocity.z = newVel.z;

		if (groundVelocity.magnitude > currentValues.maxHorizontalSpeed)
		{
			groundVelocity = groundVelocity.normalized * currentValues.maxHorizontalSpeed;
			newVel.x = groundVelocity.x;
			newVel.z = groundVelocity.z;
			pawn.body.rigidbody.velocity = newVel;
		}

		pawn.body.rigidbody.velocity += transform.TransformVector(_localLocomotionVector) * currentValues.acceleration * Time.fixedDeltaTime;
		pawn.body.rigidbody.velocity += _globalLocomotionVector * currentValues.acceleration * Time.fixedDeltaTime;

		_preFixedLocalVelocity = localVelocity;

		// var animator = pawn.graphics.skinInstance.pawnAnimator;
		// if (animator.enabled)
		// localVelocity = animator.enabled
			// ? transform.InverseTransformVector(pawn.graphics.skinInstance.animator.value.rootPositionDelta + pawn.body.rigidbody.velocity)
			// : transform.InverseTransformVector(pawn.body.rigidbody.velocity);
		localVelocity = transform.InverseTransformVector(pawn.graphics.skinInstance.pawnAnimator.rootPositionDelta + pawn.body.rigidbody.velocity);

		var localVel2D = localVelocity;
		localVel2D.y = 0;

		var newLocalVelocityPercentage = localVel2D.normalized * Mathf.InverseLerp(0, currentValues.maxHorizontalSpeed, localVel2D.magnitude);
		newLocalVelocityPercentage.y = localVelocity.y;
		localVelocityPercentage = newLocalVelocityPercentage;
	}

	private void SetMoving(bool isMoving)
	{
		if (!this.isMoving && isMoving)
		{
			this.isMoving = true;
			pawn.events.onStartMoving.Invoke();
		}
		else if (this.isMoving && !isMoving)
		{
			this.isMoving = false;
			pawn.events.onStopMoving.Invoke();
		}
	}

	public void OnUpdate()
	{
		float alpha = (Time.time - Time.fixedTime) / Time.fixedDeltaTime;
		interpolatedLocalVelocity = Vector3.Lerp(_preFixedLocalVelocity, localVelocity, alpha);
		interpolatedGroundVelocity = Vector3.Lerp(_preFixedGroundVelocity, groundVelocity, alpha);
	}

	public void StopMoving()
	{
		SetMoving(false);
		_localLocomotionVector = Vector3.zero;
		_globalLocomotionVector = Vector3.zero;
	}

	public void MoveLocal(Vector3 vec)
	{
		SetMoving(true);
		_localLocomotionVector = vec;
	}

	public void SnapLocal(Vector3 vec)
	{
		SetMoving(true);
		_localLocomotionVector = vec;
		_localLocomotionVector = vec;
	}

	public void MoveGlobal(Vector3 vec)
	{
		SetMoving(true);
		_globalLocomotionVector = vec;
	}

	public void MoveForward()
	{
		SetMoving(true);
		_localLocomotionVector = Vector3.forward;
	}

	public void MoveTowards(Vector3 targetPoint)
	{
		SetMoving(true);
		_globalLocomotionVector = (targetPoint - transform.position).normalized;
	}

	public void GroundJump()
	{
		if (pawn.hasGroundDetector && !pawn.groundDetector.isGrounded)
			return;

		Jump();
	}

	public void Jump()
	{
		var newVelocity = pawn.body.rigidbody.velocity;
		newVelocity.y = config.jumpForce;
		pawn.body.rigidbody.velocity = newVelocity;

		pawn.events.onJump.Invoke();
	}
}
