using UnityEngine;
using Gruffdev.BCS;
using System;

[AddComponentMenu("Pawn/Player")]
public class PawnPlayerSystem : PawnSystem<PawnPlayerConfig>
{
	public static PawnPlayerSystem I;

	public override void Init(Pawn pawn, PawnPlayerConfig config)
	{
		I = this;

		base.Init(pawn, config);
	}

	public override void LateSetup()
	{
		pawn.events.onDeath += OnDeath;
		GameManager.I.onGameStateSet += OnGameStateSet;
	}

	private void OnGameStateSet(GameManager.GameState state)
	{
		if (state == GameManager.GameState.LevelComplete)
		{
			var anim = pawn.graphics.skinInstance.pawnAnimator.animator;
			anim.SetTrigger(AnimHash.CELEBRATE);
			anim.SetInteger(AnimHash.CELEBRATE_ID, 0);

			var cameraTransform = CameraSystem.I.camera.transform;
			pawn.rotator.behaviour.LookAt(cameraTransform.position);
			pawn.lookAt.SetMode(PawnLookAtSystem.LookAtMode.Transform);
			pawn.lookAt.targetTransform = cameraTransform;
		}
	}

	private void OnDeath(Pawn pawn)
	{
		GameManager.I.SetGameState(GameManager.GameState.GameOver);
	}
}
