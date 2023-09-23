using UnityEngine;
using Gruffdev.BCS;

public partial class Pawn : MonoBehaviour, IEntity
{
	public bool hasGroundDetector { private set; get; }
	public PawnGroundDetectorSystem groundDetector { private set; get; }
	public PawnGroundDetectorConfig groundDetectorConfig { private set; get; }
	
	public PawnGroundDetectorSystem AddGroundDetector(PawnGroundDetectorConfig config)
	{
		if (hasGroundDetector)
			Destroy(groundDetector);
		
		groundDetector = gameObject.AddComponent<PawnGroundDetectorSystem>();
		groundDetectorConfig = config;
		groundDetector.Init(this, config);
		hasGroundDetector = true;
		return groundDetector;
	}
	
	public void RemoveGroundDetector()
	{
		if (!hasGroundDetector)
			return;
	
		groundDetector.Remove();
		Destroy(groundDetector);
	
		hasGroundDetector = false;
		groundDetector = null;
		groundDetectorConfig = null;
	}
}
