using UnityEngine;
using Gruffdev.BCS;
using System;
using Unity.VisualScripting;

[AddComponentMenu("Pawn/Controls")]
public class PawnControlsSystem : PawnSystem<PawnControlsConfig>
	, IUpdate
	, IFixedUpdate
{
	public bool isMoving {get; private set;}
	
	private Vector3 _movementVector = Vector3.zero;
	private Vector3 _targetLookAtPivot = Vector3.zero;
	private Vector3 _targetLookAtOffset = Vector3.zero;
	private Vector3 _movementTarget = Vector3.zero;

	private float _touchDirection = 0;

	private delegate void ControlsUpdate();
	ControlsUpdate controlsUpdate;
	ControlsUpdate controlsFixedUpdate;

	public override void Init(Pawn pawn, PawnControlsConfig config)
	{
		base.Init(pawn, config);

		controlsUpdate = NoControlsUpdate;
		controlsFixedUpdate = NoControlsUpdate;
	}

	public override void LateSetup()
	{
		_targetLookAtOffset = transform.rotation * Vector3.forward;

		ControlsManager.I.onPointerPressed += OnPointerPressed;
		ControlsManager.I.onPointerReleased += OnPointerReleased;

		RefreshLookAtPivot();
		pawn.lookAt.targetPoint = _targetLookAtPivot + Vector3.forward * 100;

		GameManager.I.onGameStateSet += OnGameStateSet;
	}

	private void OnGameStateSet(GameManager.GameState newState)
	{
		ControlsManager.I.onPointerPressed -= OnPointerPressed;
		ControlsManager.I.onPointerReleased -= OnPointerReleased;
		
		if (newState == GameManager.GameState.Gameplay)
		{
			controlsUpdate = NormalControlsUpdate;
			controlsFixedUpdate = NormalControlsFixedUpdate;
			ControlsManager.I.onPointerPressed += OnPointerPressed;
			ControlsManager.I.onPointerReleased += OnPointerReleased;
		}
		else
		{
			controlsUpdate = NoControlsUpdate;
			controlsFixedUpdate = NoControlsUpdate;
			isMoving = false;
			_movementVector = Vector3.zero;
			_touchDirection = 0;

			pawn.physics.StopMoving();

			// ResetIdleLookAtTarget();
		}
	}

	private void NoControlsUpdate() { }
	private void NormalControlsUpdate()
	{
		var isPointerDown = ControlsManager.I.isPointerDown;

		var rawMovementVector = isPointerDown
			? ControlsManager.I.touchMoveDelta
			: ControlsManager.I.movementVector;

		isMoving = isPointerDown || rawMovementVector.magnitude > 0.2f;

		if (isPointerDown)
		{
			_touchDirection += rawMovementVector.x * Time.deltaTime;
			_touchDirection = Mathf.Clamp(_touchDirection, -1f, 1f);

			_touchDirection -= _touchDirection * 3.5f * Time.deltaTime;
		}

		_movementVector.x = isPointerDown
			? _touchDirection
			: rawMovementVector.x;
		_movementVector.z = isMoving ? 1 : 0;
	}
	private void NormalControlsFixedUpdate()
	{
		if (isMoving)
		{		
			_movementTarget = _targetLookAtOffset = Quaternion.Euler(0, CameraSystem.I.camera.transform.eulerAngles.y, 0) * _movementVector;
			Vector3 targetPoint = pawn.transform.position + _movementTarget;
			_targetLookAtOffset = _movementTarget;

			// Move into direction
			pawn.physics.MoveForward();

			// Look towards target
			pawn.rotator.behaviour.LookAt(targetPoint);
			targetPoint.y = pawn.graphics.skinInstance.bones.head.transform.position.y;
			pawn.lookAt.targetPoint = targetPoint;
			pawn.rotator.behaviour.SnapGoalDirection();
			
			pawn.lookAt.targetPoint = _targetLookAtPivot + _targetLookAtOffset * 100;
		}
		else
		{
			pawn.physics.StopMoving();

			pawn.rotator.behaviour.LookAt(transform.position + Vector3.forward);
			pawn.rotator.behaviour.SnapGoalDirection();
		}
		
		RefreshLookAtPivot();
	}

	public void OnUpdate() => controlsUpdate();
	public void OnFixedUpdate() => controlsFixedUpdate();

	private void RefreshLookAtPivot()
	{
		_targetLookAtPivot.x = transform.position.x;
		_targetLookAtPivot.z = transform.position.z;
		_targetLookAtPivot.y = pawn.graphics.skinInstance.bones.head.transform.position.y;
	}

	private void OnPointerReleased()
	{
		_touchDirection = 0;
		ResetIdleLookAtTarget();
	}

	private void OnPointerPressed()
	{
		_touchDirection = 0;

		pawn.lookAt.SetMode(PawnLookAtSystem.LookAtMode.Point);
		pawn.lookAt.targetPoint = _targetLookAtPivot + Vector3.forward * 100;
	}

	public void ResetIdleLookAtTarget()
	{
		bool interestingObjectFound = false;

		Collider[] nearbyTriggers = Physics.OverlapSphere(transform.position, 4, CommonAssetsData.I.layerMasks.triggers);
		
		if (nearbyTriggers.Length > 0)
		{
			Collider nearestTrigger = null;
			float minDist = Mathf.Infinity;
			Vector3 myPos = transform.position;

			foreach (var trig in nearbyTriggers)
			{
				var trigPos = trig.transform.position;
				float dist = Vector3.Distance(trigPos, myPos);

				if (dist < minDist && trigPos.z >= myPos.z + 0.25f)
				{
					nearestTrigger = trig;
					minDist = dist;
				}
			}

			if (nearestTrigger && nearestTrigger.TryGetComponent<InterestingObject>(out var interestingObject))
			{
				interestingObjectFound = true;
				pawn.lookAt.SetMode(PawnLookAtSystem.LookAtMode.Transform);
				pawn.lookAt.targetTransform = interestingObject.lookAtTransform.transform;
			}
		}
		
		if (!interestingObjectFound)
		{
			pawn.lookAt.SetMode(PawnLookAtSystem.LookAtMode.Point);
			pawn.lookAt.targetPoint = _targetLookAtPivot + Vector3.forward * 100;
		}
	}
}