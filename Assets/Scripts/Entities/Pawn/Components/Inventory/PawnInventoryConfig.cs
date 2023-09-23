using UnityEngine;
using Gruffdev.BCS;

[CreateAssetMenu(fileName = "Inventory", menuName = "Data/Pawn/Inventory")]
public class PawnInventoryConfig : PawnComponentConfig
{
	public override void ConstructSystemComponent(Pawn entityObject)
	{
		entityObject.AddInventory(this);
	}
}
