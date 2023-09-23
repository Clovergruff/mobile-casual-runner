using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gruffdev.BCS;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PickupSpawner : Spawner
{
	[SerializeField] private PickupConfig pickupData;

	public override void Spawn() => SpawnPickup();

	public Pickup SpawnPickup()
	{
		if (pickupData)
		{
			var pickupInstance = PickupFactory.Create(pickupData, transform.position, transform.rotation);
			return pickupInstance;
		}

		return null;
	}
}