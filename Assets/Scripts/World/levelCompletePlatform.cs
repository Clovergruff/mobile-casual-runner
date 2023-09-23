using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelCompletePlatform : MonoBehaviour
{
	public ParticleSystem finishConfettiParticles;

	private void Start()
	{
		GameManager.I.onGameStateSet += OnGameStateSet;
	}

	private void OnGameStateSet(GameManager.GameState state)
	{
		if (state == GameManager.GameState.LevelComplete)
		{
			finishConfettiParticles.Play(true);
		}
	}
}
