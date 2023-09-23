using UnityEngine;
using Gruffdev.BCS;

public static class PawnFactory
{
	public static Pawn CreatePooled(PawnConfig config, Vector3 position, Quaternion rotation, Transform parent = null)
	{
		if (PawnEntityManager.I.inactiveEntities.Count > 0)
		{
			Pawn entity = PawnEntityManager.I.inactiveEntities[0];
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

	public static Pawn Create(PawnConfig config, Vector3 position, Quaternion rotation, Transform parent = null)
	{
		// Create the Pawn game object
		Pawn entity = new GameObject(config.name).AddComponent<Pawn>();

		
		Transform transform = entity.transform;
		transform.position = position;
		transform.rotation = rotation;
		transform.SetParent(parent);

		// Construct the Pawn MonoBehaviour components
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
