using UnityEngine;
using Gruffdev.BCS;

[CreateAssetMenu(fileName = "Graphics", menuName = "Data/Pawn/Graphics")]
public class PawnGraphicsConfig : PawnComponentConfig
{
	[Space]
	public PawnSkinData skinData;
	public float scale = 1;
	public float turnLeanMultiplier = 1;
	[Range(0, 90)]
	public float maxLeanAngle = 25;
	
	public override void ConstructSystemComponent(Pawn entityObject)
	{
		entityObject.AddGraphics(this);
	}
}
