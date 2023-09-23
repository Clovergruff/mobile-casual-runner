using UnityEngine;
using Gruffdev.BCS;

[AddComponentMenu("Pickup/Stats")]
public class PickupStatsSystem : PickupSystem<PickupStatsConfig>
{
	public override void Init(Pickup pickup, PickupStatsConfig config)
	{
		base.Init(pickup, config);
	}
}
