using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreCounterUI : MonoBehaviour
{
	public TMP_Text scoreText;

	private void Start()
	{
		GameManager.I.onScoreChanged += RefreshScore;
		RefreshScore(GameManager.currentScore);
	}

	private void RefreshScore(int newScore)
	{
		scoreText.SetText(newScore.ToString());
	}
}
