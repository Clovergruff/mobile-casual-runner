using UnityEngine;
using Gruffdev.BCS;
using System;

[AddComponentMenu("Pickup/Events")]
public class PickupEventsSystem : PickupSystem<PickupEventsConfig>
{
	public Action<Pawn> onCollected = x => {};
	public Action onCollectionAnimationEnd = () => {};

	public override void Init(Pickup pickup, PickupEventsConfig config)
	{
		base.Init(pickup, config);
	}
}
