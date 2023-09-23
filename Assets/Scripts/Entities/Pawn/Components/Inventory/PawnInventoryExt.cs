using UnityEngine;
using Gruffdev.BCS;

public partial class Pawn : MonoBehaviour, IEntity
{
	public bool hasInventory { private set; get; }
	public PawnInventorySystem inventory { private set; get; }
	public PawnInventoryConfig inventoryConfig { private set; get; }
	
	public PawnInventorySystem AddInventory(PawnInventoryConfig config)
	{
		if (hasInventory)
			Destroy(inventory);
		
		inventory = gameObject.AddComponent<PawnInventorySystem>();
		inventoryConfig = config;
		inventory.Init(this, config);
		hasInventory = true;
		return inventory;
	}
	
	public void RemoveInventory()
	{
		if (!hasInventory)
			return;
	
		inventory.Remove();
		Destroy(inventory);
	
		hasInventory = false;
		inventory = null;
		inventoryConfig = null;
	}
}
