using UnityEngine;

public interface ITriggerable
{
    PawnConfig.PawnType CompatiblePawns { get; set; }
	
	void Enter(PawnTriggerDetectorSystem detector);
	void Exit(PawnTriggerDetectorSystem detector);
}
