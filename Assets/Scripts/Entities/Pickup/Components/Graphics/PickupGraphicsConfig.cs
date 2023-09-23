using UnityEngine;
using Gruffdev.BCS;

[CreateAssetMenu(fileName = "Graphics", menuName = "Data/Pickup/Graphics")]
public class PickupGraphicsConfig : PickupComponentConfig
{
	public PickupSkinInstance skinPrefab;
	public float scale = 1;

	public override void ConstructSystemComponent(Pickup entityObject)
	{
		entityObject.AddGraphics(this);
	}
}
