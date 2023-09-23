using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
	public enum ScreenType
	{
		MainMenu,
		Gameplay,
		Pause,
		LevelComplete,
		GameOver,
	}

	public Canvas mainCamvas;
	public Screens screens;

	private Dictionary<ScreenType, UIScreen> allScreens = new Dictionary<ScreenType, UIScreen>();

	private void Awake()
	{
		allScreens.Add(ScreenType.MainMenu, screens.mainMenu);
		allScreens.Add(ScreenType.Gameplay, screens.gameplay);
		allScreens.Add(ScreenType.Pause, screens.pause);
		allScreens.Add(ScreenType.LevelComplete, screens.levelComplete);
		allScreens.Add(ScreenType.GameOver, screens.gameOver);

		SetCurrentScreen(ScreenType.MainMenu);
	}

	private void Start()
	{
		GameManager.I.onGameStateSet += OnGameStateSet;
	}

	private void OnGameStateSet(GameManager.GameState state)
	{
		switch (state)
		{
			case GameManager.GameState.MainMenu:
				SetCurrentScreen(ScreenType.MainMenu);
				break;
			case GameManager.GameState.Gameplay:
				SetCurrentScreen(ScreenType.Gameplay);
				break;
			case GameManager.GameState.GameOver:
				SetCurrentScreen(ScreenType.GameOver);
				break;
			case GameManager.GameState.LevelComplete:
				SetCurrentScreen(ScreenType.LevelComplete);
				break;
		}
	}

	public void SetCurrentScreen(ScreenType screenType)
	{
		foreach (var screen in allScreens)
		{
			if (screen.Key == screenType)
				screen.Value.Open();
			else
				screen.Value.Close();
		}
	}

	[System.Serializable]
	public class Screens
	{
		public MainMenuUIScreen mainMenu;
		public GameplayUIScreen gameplay;
		public PauseUIScreen pause;
		public LevelCompleteUIScreen levelComplete;
		public GameOverUIScreen gameOver;
	}
}
