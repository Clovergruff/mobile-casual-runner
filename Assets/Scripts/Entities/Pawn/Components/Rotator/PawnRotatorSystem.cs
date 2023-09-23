using UnityEngine;
using Gruffdev.BCS;
using static PawnRotatorConfig;
using System;


[AddComponentMenu("Pawn/Rotator")]
public class PawnRotatorSystem : PawnSystem<PawnRotatorConfig>
	, IFixedUpdate
{
public enum TurnDirection
	{
		Left = -1,
		Right = 1,
	}

	public RotationModeType type = RotationModeType.Continuous;

	public PawnRotatorBehaviour behaviour {get; private set;}

	public override void Init(Pawn pawn, PawnRotatorConfig config)
	{
		base.Init(pawn, config);
        SetRotationMode(config.rotationMode, true);
        SetRotationSpeed(config.defaultSpeed);

		behaviour.SnapDirection(transform.eulerAngles.y);
	}

	public override void LateSetup()
	{
		pawn.events.onJump += OnJump;
	}

	public void OnFixedUpdate()
	{
		behaviour.Update();
	}

	public void SetRotationMode(RotationModeType newType, bool force = false)
	{
		if (type == newType && !force)
			return;

		type = newType;

		switch (type)
		{
			case RotationModeType.None:           behaviour = new PawnRotatorBehaviour();				break;
			case RotationModeType.Continuous:     behaviour = new ContinuousPawnRotatorBehaviour();		break;
			case RotationModeType.Incremental:    behaviour = new IncrementalPawnRotatorBehaviour();	break;
		}

		behaviour.Setup(this);
	}

	public void SetRotationSpeed(float speed, float maxSpeed)
	{
		behaviour.SetSpeed(speed);
		behaviour.SetMaxSpeed(maxSpeed);
	}
	public void SetRotationSpeed(float speed) => behaviour.SetSpeed(speed);
	public void SetMaxRotationSpeed(float maxSpeed) => behaviour.SetMaxSpeed(maxSpeed);

	public class PawnRotatorBehaviour
	{
		public float direction {get; protected set;}
		public float directionTarget {get; protected set;}
		public float turnDelta {get; protected set;}
		public float speed {get; protected set;}
		public float maxSpeed {get; protected set;}

		protected PawnRotatorSystem rotator;
		protected float directionGoal;

		public void Setup(PawnRotatorSystem rotator)
		{
			this.rotator = rotator;
			speed = rotator.config.defaultSpeed;
			maxSpeed = rotator.config.defaultMaxSpeed;
		}

		public virtual void Update() {}

		public void SetSpeed(float speed) => this.speed = speed;
		public void SetMaxSpeed(float maxSpeed) => this.maxSpeed = maxSpeed;
		public void SetSpeed(float speed, float maxSpeed)
		{
			this.speed = speed;
			this.maxSpeed = maxSpeed;
		}

		public void SnapDirection(float direction)
		{
			this.direction = direction;
			directionTarget = direction;
			directionGoal = direction;
		}

		public void SnapGoalDirection() => directionTarget = directionGoal;
		public void LookInDirection(float targetDirection) => directionGoal = targetDirection;
		public void LookAt(Vector3 target) => directionGoal = Quaternion.LookRotation((target - rotator.transform.position).normalized, rotator.transform.up).eulerAngles.y;
		public void LookTowards(Vector3 forward) => directionGoal = Quaternion.LookRotation(forward).eulerAngles.y;
		public void LookTowards(Quaternion forwardRotation) => directionGoal = Quaternion.LookRotation(forwardRotation * Vector3.forward).eulerAngles.y;

		protected void ApplyRotation()
		{
			float oldDirection = direction;
			// direction = Mathf.LerpAngle(direction, directionTarget, Mathf.Clamp(speed * Time.fixedDeltaTime, 0, maxSpeed));
			direction = Mathf.LerpAngle(direction, directionTarget, speed * Time.fixedDeltaTime);
			turnDelta = Mathf.DeltaAngle(direction, oldDirection);
			// turnDelta = Mathf.DeltaAngle(direction, directionTarget);
			rotator.transform.rotation = Quaternion.Euler(0, direction, 0);
		}
	}

	public class ContinuousPawnRotatorBehaviour : PawnRotatorBehaviour
	{
		public override void Update()
		{
			SnapGoalDirection();
			ApplyRotation();
		}
	}

	public class IncrementalPawnRotatorBehaviour : PawnRotatorBehaviour
	{
		public override void Update()
		{
			float directionGoalDelta = Mathf.DeltaAngle(directionGoal, direction);

			if (Mathf.Abs(directionGoalDelta) > rotator.config.incrementSize)
				directionTarget = directionGoal;

			ApplyRotation();
		}
	}

	private void OnJump()
	{
		behaviour.SnapGoalDirection();
	}
}
