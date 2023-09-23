using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnSlidingState : PawnPhysicsState
{
	private const float DAMPING = 5;

	public override void OnEnable()
	{
		// physicsSystem.Body.Rigidbody.velocity = Vector3.zero;
	}

	public override void FixedUpdate()
	{
		var rigidbody = physicsSystem.pawn.body.rigidbody;
		var groundDetector = physicsSystem.pawn.groundDetector;

		Vector3 newVelocity = rigidbody.velocity;

		newVelocity += Physics.gravity * Time.fixedDeltaTime;

		newVelocity.x += groundDetector.groundHit.normal.x * 7 * Time.fixedDeltaTime;
		newVelocity.z += groundDetector.groundHit.normal.z * 7 * Time.fixedDeltaTime;
		newVelocity.y += (1 - groundDetector.groundHit.normal.y) * 7 * Time.fixedDeltaTime;

		newVelocity.x -= newVelocity.x * DAMPING * Time.fixedDeltaTime;
		newVelocity.z -= newVelocity.z * DAMPING * Time.fixedDeltaTime;

		rigidbody.velocity = newVelocity;
	}
}