using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PawnPhysicsState
{
	protected PawnPhysicsSystem physicsSystem;

	public void Setup(PawnPhysicsSystem physicsSystem)
	{
		this.physicsSystem = physicsSystem;
	}

	public abstract void FixedUpdate();

	public virtual void OnDisable() { }
	public virtual void OnEnable() { }

	protected void SetPhysicsProfileValues(PawnPhysicsConfig.PawnPhysicsDataProfile profileValues)
	{
		var pawn = physicsSystem.pawn;

		if (pawn.hasRotator)
			physicsSystem.pawn.rotator.SetRotationSpeed(profileValues.rotationSpeed, profileValues.rotationMaxSpeed);

		if (pawn.hasLookAt)
			physicsSystem.pawn.lookAt.adjustSpeed = profileValues.lookatSpeed;
	}
}