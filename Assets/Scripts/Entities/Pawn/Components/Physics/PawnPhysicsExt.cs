using UnityEngine;
using Gruffdev.BCS;

public partial class Pawn : MonoBehaviour, IEntity
{
	public bool hasPhysics { private set; get; }
	public PawnPhysicsSystem physics { private set; get; }
	public PawnPhysicsConfig physicsConfig { private set; get; }
	
	public PawnPhysicsSystem AddPhysics(PawnPhysicsConfig config)
	{
		if (hasPhysics)
			Destroy(physics);
		
		physics = gameObject.AddComponent<PawnPhysicsSystem>();
		physicsConfig = config;
		physics.Init(this, config);
		hasPhysics = true;
		return physics;
	}
	
	public void RemovePhysics()
	{
		if (!hasPhysics)
			return;
	
		physics.Remove();
		Destroy(physics);
	
		hasPhysics = false;
		physics = null;
		physicsConfig = null;
	}
}
