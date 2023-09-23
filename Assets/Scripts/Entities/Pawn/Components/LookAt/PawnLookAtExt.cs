using UnityEngine;
using System.Collections;
using Gruffdev.BCS;

public partial class Pawn : MonoBehaviour, IEntity
{
	public bool hasLookAt { private set; get; }
	public PawnLookAtSystem lookAt { private set; get; }
	public PawnLookAtConfig lookAtConfig { private set; get; }
	
	public PawnLookAtSystem AddLookAt(PawnLookAtConfig config)
	{
		if (hasLookAt)
			Destroy(lookAt);
		
		lookAt = gameObject.AddComponent<PawnLookAtSystem>();
		lookAtConfig = config;
		lookAt.Init(this, config);
		hasLookAt = true;
		return lookAt;
	}
	
	public void RemoveLookAt()
	{
		if (!hasLookAt)
			return;
	
		lookAt.Remove();
		Destroy(lookAt);
	
		hasLookAt = false;
		lookAt = null;
		lookAtConfig = null;
	}
}
