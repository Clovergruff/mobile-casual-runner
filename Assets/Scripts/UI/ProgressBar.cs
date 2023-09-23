using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
	public TMP_Text[] percentageText;
	public Image fillImage;

	private void LateUpdate()
	{
		var progress = GameManager.I.GetCurrentProgress();

		fillImage.fillAmount = progress;
		foreach (var text in percentageText)
			text.SetText($"{(progress * 100).ToString("F0")}%");
	}
}
