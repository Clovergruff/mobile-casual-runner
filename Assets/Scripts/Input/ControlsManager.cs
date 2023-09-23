using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlsManager : MonoBehaviour
{
	public static ControlsManager I { get; private set; }

	public Vector2 touchMoveDelta;
	public Vector2 movementVector;
	public bool isPointerDown;

	public Action onPointerPressed = () => {};
	public Action onPointerReleased = () => {};

	private GameInput input = null;

	private void Awake()
	{
		I = this;
		input = new GameInput();
	}

	private void Update()
	{
		touchMoveDelta = input.Player.TouchMovement.ReadValue<Vector2>();
		movementVector = input.Player.PhysicalMovement.ReadValue<Vector2>();
	}

	private void OnEnable()
	{
		input.Enable();
		input.Player.TouchMovement.performed += OnTouchMoved;
		input.Player.PhysicalMovement.started += OnMovementStarted;
		input.Player.PhysicalMovement.canceled += OnMovementStopped;

		input.Player.Touched.started += OnPointerPressed;
		input.Player.Touched.canceled += OnPointerReleased;
	}


	private void OnDisable()
	{
		input.Disable();
		input.Player.TouchMovement.performed -= OnTouchMoved;
		input.Player.PhysicalMovement.started -= OnMovementStarted;
		input.Player.PhysicalMovement.canceled -= OnMovementStopped;
	}

	private void OnTouchMoved(InputAction.CallbackContext context) {}
	private void OnMovementStarted(InputAction.CallbackContext context) {}
	private void OnMovementStopped(InputAction.CallbackContext context) {}
	
	private void OnPointerPressed(InputAction.CallbackContext context)
	{
		isPointerDown = true;
		onPointerPressed.Invoke();
	}
	private void OnPointerReleased(InputAction.CallbackContext context)
	{
		isPointerDown = false;
		onPointerReleased.Invoke();
	}
}
