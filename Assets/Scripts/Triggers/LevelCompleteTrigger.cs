using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PawnConfig;

public class LevelCompleteTrigger : MonoBehaviour, ITriggerable
{
	[SerializeField] private PawnType compatiblePawns = PawnType.Player;

	public PawnType CompatiblePawns 	{ get { return compatiblePawns; } set { compatiblePawns = value; } }

	public void Enter(PawnTriggerDetectorSystem detector)
	{
		GameManager.I.SetGameState(GameManager.GameState.LevelComplete);
	}

	public void Exit(PawnTriggerDetectorSystem detector)
	{
	}
}
