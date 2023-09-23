using UnityEngine;
using Gruffdev.BCS;

public partial class Pickup : MonoBehaviour, IEntity
{
	public bool hasGraphics { private set; get; }
	public PickupGraphicsSystem graphics { private set; get; }
	public PickupGraphicsConfig graphicsConfig { private set; get; }
	
	public PickupGraphicsSystem AddGraphics(PickupGraphicsConfig config)
	{
		if (hasGraphics)
			Destroy(graphics);
		
		graphics = gameObject.AddComponent<PickupGraphicsSystem>();
		graphicsConfig = config;
		graphics.Init(this, config);
		hasGraphics = true;
		return graphics;
	}
	
	public void RemoveGraphics()
	{
		if (!hasGraphics)
			return;
	
		graphics.Remove();
		Destroy(graphics);
	
		hasGraphics = false;
		graphics = null;
		graphicsConfig = null;
	}
}
