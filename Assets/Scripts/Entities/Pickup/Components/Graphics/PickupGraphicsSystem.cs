using UnityEngine;
using Gruffdev.BCS;
using System;

[AddComponentMenu("Pickup/Graphics")]
public class PickupGraphicsSystem : PickupSystem<PickupGraphicsConfig>
{
	public PickupSkinInstance skinInstance {get; private set;}

	public override void Init(Pickup pickup, PickupGraphicsConfig config)
	{
		base.Init(pickup, config);
	}

	public override void LateSetup()
	{
		ApplySkin(config.skinPrefab);
		pickup.events.onCollected += OnCollected;
	}

	public void ApplySkin(PickupSkinInstance skinPrefab)
	{
		ClearSkin();

		skinInstance = Instantiate(skinPrefab, transform.position, transform.rotation, transform);
		skinInstance.transform.localScale = Vector3.one;
		skinInstance.Init(this);
	}

	public void ClearSkin()
	{
		if (skinInstance)
			Destroy(skinInstance.gameObject);
	}

	private void OnCollected(Pawn pawn)
	{
		skinInstance.triggerCollider.enabled = false;
	}
}
