using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelCounterUI : MonoBehaviour
{
	public TMP_Text levelText;

	private void Start()
	{
		levelText.SetText($"LVL {GameManager.currentLevel + 1}");
	}
}
