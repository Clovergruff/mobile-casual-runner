using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUIScreen : UIScreen
{
	public void OnRestartButtonClick()
	{
		GameManager.I.ResetScene();
	}
}
