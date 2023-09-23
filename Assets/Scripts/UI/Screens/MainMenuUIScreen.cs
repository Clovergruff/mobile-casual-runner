using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIScreen : UIScreen
{
	public void OnPlayTapped()
	{
		GameManager.I.SetGameState(GameManager.GameState.Gameplay);
	}
}
