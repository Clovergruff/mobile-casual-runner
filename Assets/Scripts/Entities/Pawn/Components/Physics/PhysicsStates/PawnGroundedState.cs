using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnGroundedState : PawnPhysicsState
{
	private const float DAMPING = 5f;

	public override void OnEnable()
	{
		var pawn = physicsSystem.pawn;
		// physicsSystem.Body.Rigidbody.velocity = Vector3.zero;
		pawn.body.rigidbody.useGravity = false;
		// pawn.events.onGrounded.Invoke();
		
		SetPhysicsProfileValues(physicsSystem.Config.groundedValues);
	}

	public override void FixedUpdate()
	{
		var rigidbody = physicsSystem.pawn.body.rigidbody;

		Vector3 newVelocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);

		newVelocity.x -= newVelocity.x * physicsSystem.Config.groundedValues.damping * Time.fixedDeltaTime;
		newVelocity.z -= newVelocity.z * physicsSystem.Config.groundedValues.damping * Time.fixedDeltaTime;

		rigidbody.velocity = newVelocity;
	}
}