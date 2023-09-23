using UnityEngine;
using Gruffdev.BCS;

public partial class Pickup : MonoBehaviour, IEntity
{
	public bool hasCollection { private set; get; }
	public PickupCollectionSystem collection { private set; get; }
	public PickupCollectionConfig collectionConfig { private set; get; }
	
	public PickupCollectionSystem AddCollection(PickupCollectionConfig config)
	{
		if (hasCollection)
			Destroy(collection);
		
		collection = gameObject.AddComponent<PickupCollectionSystem>();
		collectionConfig = config;
		collection.Init(this, config);
		hasCollection = true;
		return collection;
	}
	
	public void RemoveCollection()
	{
		if (!hasCollection)
			return;
	
		collection.Remove();
		Destroy(collection);
	
		hasCollection = false;
		collection = null;
		collectionConfig = null;
	}
}
