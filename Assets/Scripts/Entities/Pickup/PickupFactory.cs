using UnityEngine;
using Gruffdev.BCS;

public static class PickupFactory
{
	public static Pickup CreatePooled(PickupConfig config, Vector3 position, Quaternion rotation, Transform parent = null)
	{
		if (PickupEntityManager.I.inactiveEntities.Count > 0)
		{
			Pickup entity = PickupEntityManager.I.inactiveEntities[0];
			entity.gameObject.SetActive(true);
		
			Transform transform = entity.transform;
			transform.position = position;
			transform.rotation = rotation;
			transform.SetParent(parent);

			foreach (var system in entity.allSystems)
				system.ReusedSetup();

			return entity;
		}
		else
		{
			return Create(config, position, rotation, parent);
		}
	}

	public static Pickup Create(PickupConfig config, Vector3 position, Quaternion rotation, Transform parent = null)
	{
		// Create the Pickup game object
		Pickup entity = new GameObject(config.name).AddComponent<Pickup>();

		
		Transform transform = entity.transform;
		transform.position = position;
		transform.rotation = rotation;
		transform.SetParent(parent);

		// Construct the Pickup MonoBehaviour components
		foreach (var componentConfig in config.components)
		{
			if (componentConfig == null)
			{
				Debug.LogError($"{config.name} has a NULL item in its component list. Consider removing it.");
				continue;
			}

			componentConfig.ConstructSystemComponent(entity);
		}

		entity.FindSystems();

		foreach (var system in entity.allSystems)
			system.LateSetup();

		entity.Init(config);

		return entity;
	}
}
