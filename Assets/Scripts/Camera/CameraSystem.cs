using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class CameraSystem : MonoBehaviour
{
	public static CameraSystem I {get; private set;}
	
	public new Camera camera;

	[Space]
	public CommonCameras commonCameras;

	private void Awake()
	{
		I = this;

#if UNITY_EDITOR
		Application.targetFrameRate = 200;
#else
		Application.targetFrameRate = 120;
#endif
	}

	private void Start()
	{
		if (PawnPlayerSystem.I)
		{
			var playerPawn = PawnPlayerSystem.I.pawn;
			OnPlayerSkinApplied(playerPawn.graphics.skinInstance);
			playerPawn.events.onSkinApplied += OnPlayerSkinApplied;
		}

		GameManager.I.onGameStateSet += OnGameStateSet;
	}

	private void OnGameStateSet(GameManager.GameState state)
	{
		switch (state)
		{
			case GameManager.GameState.MainMenu:
				commonCameras.levelComplete.Priority.Value = 0;
				break;
			case GameManager.GameState.Gameplay:
				commonCameras.levelComplete.Priority.Value = 0;
				break;
			case GameManager.GameState.GameOver:
				commonCameras.levelComplete.Priority.Value = 0;
				break;
			case GameManager.GameState.LevelComplete:
				commonCameras.levelComplete.Priority.Value = 20;
				break;
		}
	}

	private void OnPlayerSkinApplied(PawnSkinInstance skinInstance) => SetNewPlayerTarget(skinInstance);
	private void SetNewPlayerTarget(PawnSkinInstance skinInstance)
	{
		commonCameras.normal.Follow = skinInstance.GetCameraFollowTarget();
		commonCameras.levelComplete.Follow = skinInstance.GetCameraFollowTarget();
	}

	[System.Serializable]
	public struct CommonCameras
	{
		public CinemachineCamera normal;
		public CinemachineCamera levelComplete;
	}
}