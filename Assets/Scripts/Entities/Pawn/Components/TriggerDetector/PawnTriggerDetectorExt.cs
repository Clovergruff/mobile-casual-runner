using UnityEngine;
using Gruffdev.BCS;

public partial class Pawn : MonoBehaviour, IEntity
{
	public bool hasTriggerDetector { private set; get; }
	public PawnTriggerDetectorSystem triggerDetector { private set; get; }
	public PawnTriggerDetectorConfig triggerDetectorConfig { private set; get; }
	
	public PawnTriggerDetectorSystem AddTriggerDetector(PawnTriggerDetectorConfig config)
	{
		if (hasTriggerDetector)
			Destroy(triggerDetector);
		
		triggerDetector = gameObject.AddComponent<PawnTriggerDetectorSystem>();
		triggerDetectorConfig = config;
		triggerDetector.Init(this, config);
		hasTriggerDetector = true;
		return triggerDetector;
	}
	
	public void RemoveTriggerDetector()
	{
		if (!hasTriggerDetector)
			return;
	
		triggerDetector.Remove();
		Destroy(triggerDetector);
	
		hasTriggerDetector = false;
		triggerDetector = null;
		triggerDetectorConfig = null;
	}
}
