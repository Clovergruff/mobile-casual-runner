using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnGlidingState : PawnPhysicsState
{
	public override void FixedUpdate()
	{
		var rigidbody = physicsSystem.pawn.body.rigidbody;

		Vector3 newVelocity = rigidbody.velocity;
		newVelocity += Physics.gravity * Time.fixedDeltaTime;

		newVelocity.x -= newVelocity.x * physicsSystem.currentValues.damping * Time.fixedDeltaTime;
		newVelocity.z -= newVelocity.z * physicsSystem.currentValues.damping * Time.fixedDeltaTime;
		
		if (newVelocity.y < 0)
			newVelocity.y -= newVelocity.y * 10 * Time.fixedDeltaTime;

		rigidbody.velocity = newVelocity;
	}
}
