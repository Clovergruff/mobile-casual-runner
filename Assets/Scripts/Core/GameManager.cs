using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public enum GameState
	{
		MainMenu,
		Gameplay,
		GameOver,
		LevelComplete,
	}

	public static GameManager I {get; private set;}
	public static int currentLevel = 0;
	public static int currentScore = 0;

	public GameState currentState;

	public Action<GameState> onGameStateSet = x => {};
	public Action<int> onScoreChanged = x => {};

	private void Awake()
	{
		I = this;

		var levelPrefabs = CommonAssetsData.I.levelPrefabs;
		Instantiate(levelPrefabs[currentLevel % levelPrefabs.Length]);
	}

	public void SetGameState(GameState newState)
	{
		currentState = newState;
		onGameStateSet.Invoke(newState);
	}

	public void AddScore(int amount)
	{
		currentScore += amount;
		onScoreChanged.Invoke(currentScore);
	}

	public void ResetScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	
	public void GotoNextLevel()
	{
		currentLevel++;
		ResetScene();
	}

	public float GetCurrentProgress()
	{
		var level = LevelInstance.I;
		return Mathf.InverseLerp(level.startPoint.position.z, level.endPoint.position.z, PawnPlayerSystem.I.transform.position.z);
	}
}
