using UnityEngine;
using Gruffdev.BCS;

public partial class Pawn : MonoBehaviour, IEntity
{
	public bool hasHealth { private set; get; }
	public PawnHealthSystem health { private set; get; }
	public PawnHealthConfig healthConfig { private set; get; }
	
	public PawnHealthSystem AddHealth(PawnHealthConfig config)
	{
		if (hasHealth)
			Destroy(health);
		
		health = gameObject.AddComponent<PawnHealthSystem>();
		healthConfig = config;
		health.Init(this, config);
		hasHealth = true;
		return health;
	}
	
	public void RemoveHealth()
	{
		if (!hasHealth)
			return;
	
		health.Remove();
		Destroy(health);
	
		hasHealth = false;
		health = null;
		healthConfig = null;
	}
}
