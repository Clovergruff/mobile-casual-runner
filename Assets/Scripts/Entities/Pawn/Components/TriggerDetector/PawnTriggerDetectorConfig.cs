using UnityEngine;
using static PawnConfig;

[CreateAssetMenu(fileName = "TriggerDetector", menuName = "Data/Pawn/TriggerDetector")]
public class PawnTriggerDetectorConfig : PawnComponentConfig
{
	public override void ConstructSystemComponent(Pawn entityObject)
	{
		entityObject.AddTriggerDetector(this);
	}
}
