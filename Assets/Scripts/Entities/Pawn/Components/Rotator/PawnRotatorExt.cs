using UnityEngine;
using Gruffdev.BCS;

public partial class Pawn : MonoBehaviour, IEntity
{
	public bool hasRotator { private set; get; }
	public PawnRotatorSystem rotator { private set; get; }
	public PawnRotatorConfig rotatorConfig { private set; get; }
	
	public PawnRotatorSystem AddRotator(PawnRotatorConfig config)
	{
		if (hasRotator)
			Destroy(rotator);
		
		rotator = gameObject.AddComponent<PawnRotatorSystem>();
		rotatorConfig = config;
		rotator.Init(this, config);
		hasRotator = true;
		return rotator;
	}
	
	public void RemoveRotator()
	{
		if (!hasRotator)
			return;
	
		rotator.Remove();
		Destroy(rotator);
	
		hasRotator = false;
		rotator = null;
		rotatorConfig = null;
	}
}
