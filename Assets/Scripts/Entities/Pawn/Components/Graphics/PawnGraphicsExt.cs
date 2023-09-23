using UnityEngine;
using Gruffdev.BCS;

public partial class Pawn : MonoBehaviour, IEntity
{
	public bool hasGraphics { private set; get; }
	public PawnGraphicsSystem graphics { private set; get; }
	public PawnGraphicsConfig graphicsConfig { private set; get; }
	
	public PawnGraphicsSystem AddGraphics(PawnGraphicsConfig config)
	{
		if (hasGraphics)
			Destroy(graphics);
		
		graphics = gameObject.AddComponent<PawnGraphicsSystem>();
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
