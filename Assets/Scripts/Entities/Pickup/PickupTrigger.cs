using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PawnConfig;

public class PickupTrigger : MonoBehaviour, ITriggerable
{
	[SerializeField] private PawnType compatiblePawns = PawnType.Player;
	[SerializeField] private PickupSkinInstance pickupSkin;

	public PawnType CompatiblePawns 	{ get { return compatiblePawns; } set { compatiblePawns = value; } }

	public void Enter(PawnTriggerDetectorSystem detector)
	{
		pickupSkin.TriggeredByPawn(detector.pawn);
	}

	public void Exit(PawnTriggerDetectorSystem detector) { }
}
