using UnityEngine;
using Gruffdev.BCS;

[CreateAssetMenu(fileName = "Pawn", menuName = "Data/Pawn/Pawn entity")]
public class PawnConfig : EntityConfigAsset<PawnComponentConfig>
{
	[System.Flags]
	public enum PawnType
	{
		Generic = 0,
		Player = 1,
		Enemy = 2,
	}

	public PawnType type;
}