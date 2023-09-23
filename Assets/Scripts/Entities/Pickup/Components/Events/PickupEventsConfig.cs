using UnityEngine;
using Gruffdev.BCS;

[CreateAssetMenu(fileName = "Events", menuName = "Data/Pickup/Events")]
public class PickupEventsConfig : PickupComponentConfig
{
	public override void ConstructSystemComponent(Pickup entityObject)
	{
		entityObject.AddEvents(this);
	}
}
