using UnityEngine;
using Gruffdev.BCS;

[AddComponentMenu("Pawn/Inventory")]
public class PawnInventorySystem : PawnSystem<PawnInventoryConfig>
{
	public override void Init(Pawn pawn, PawnInventoryConfig config)
	{
		base.Init(pawn, config);
	}

	public override void LateSetup()
	{
	}
}
