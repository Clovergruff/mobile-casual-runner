using UnityEngine;
using Gruffdev.BCS;

[CreateAssetMenu(fileName = "Stats", menuName = "Data/Pickup/Stats")]
public class PickupStatsConfig : PickupComponentConfig
{
	public override void ConstructSystemComponent(Pickup entityObject)
	{
		entityObject.AddStats(this);
	}
}
