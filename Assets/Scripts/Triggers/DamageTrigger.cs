using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PawnConfig;

public class DamageTrigger : MonoBehaviour, ITriggerable
{
	[SerializeField] private PawnType compatiblePawns = PawnType.Player;
	[SerializeField] private int damageAmount = 1;

	public PawnType CompatiblePawns 	{ get { return compatiblePawns; } set { compatiblePawns = value; } }

	public void Enter(PawnTriggerDetectorSystem detector)
	{
		if (detector.pawn.hasHealth)
			detector.pawn.health.TakeDamage(damageAmount);
	}

	public void Exit(PawnTriggerDetectorSystem detector)
	{
	}
}
