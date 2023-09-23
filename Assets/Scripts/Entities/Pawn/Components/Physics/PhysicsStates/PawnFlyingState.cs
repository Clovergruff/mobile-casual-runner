using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnFlyingState : PawnPhysicsState
{
	private const float DAMPING = 3;

	public override void OnEnable()
	{
		// physicsSystem.Body.Rigidbody.velocity = Vector3.zero;
	}

	public override void FixedUpdate()
	{
		var rigidbody = physicsSystem.pawn.body.rigidbody;

		Vector3 newVelocity = rigidbody.velocity;

		newVelocity -= newVelocity * DAMPING * Time.fixedDeltaTime;

		rigidbody.velocity = newVelocity;
	}
}