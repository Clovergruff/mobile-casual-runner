using UnityEngine;
using Gruffdev.BCS;

[CreateAssetMenu(fileName = "Collection", menuName = "Data/Pickup/Collection")]
public class PickupCollectionConfig : PickupComponentConfig
{
	public override void ConstructSystemComponent(Pickup entityObject)
	{
		entityObject.AddCollection(this);
	}
}
