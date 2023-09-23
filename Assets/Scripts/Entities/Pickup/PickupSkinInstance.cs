using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSkinInstance : MonoBehaviour
{
	public Transform graphicsHolder;
	public Collider triggerCollider;
	public GameObject collectEffect;

	private PickupGraphicsSystem _graphicsSystem;

	public void Init(PickupGraphicsSystem pickupGraphicsSystem)
	{
		_graphicsSystem = pickupGraphicsSystem;
	}

	public void TriggeredByPawn(Pawn pawn)
	{
		_graphicsSystem.pickup.events.onCollected.Invoke(pawn);

		collectEffect.gameObject.SetActive(true);

		// This is kinda hacky, but this is just for fun, soo
		if (pawn.hasControls && !pawn.controls.isMoving)
		{
			pawn.controls.ResetIdleLookAtTarget();
		}
	}
}
