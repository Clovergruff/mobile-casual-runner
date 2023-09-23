using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteUIScreen : UIScreen
{
	public void OnRestartButtonClick()
	{
		GameManager.I.GotoNextLevel();
	}
}
