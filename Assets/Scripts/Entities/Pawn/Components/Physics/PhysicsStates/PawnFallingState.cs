using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnFallingState : PawnPhysicsState
{
	public override void OnEnable()
	{
		var pawn = physicsSystem.pawn;
		// pawn.events.onUngrounded.Invoke();
		pawn.body.rigidbody.useGravity = pawn.bodyConfig.useGravity;

		SetPhysicsProfileValues(physicsSystem.Config.midAirValues);
	}

	public override void FixedUpdate()
	{
		var rigidbody = physicsSystem.pawn.body.rigidbody;

		Vector3 newVelocity = rigidbody.velocity;
		// newVelocity += Physics.gravity * Time.fixedDeltaTime;

		newVelocity.x -= newVelocity.x * physicsSystem.Config.midAirValues.damping * Time.fixedDeltaTime;
		newVelocity.z -= newVelocity.z * physicsSystem.Config.midAirValues.damping * Time.fixedDeltaTime;

		rigidbody.velocity = newVelocity;
	}
}
