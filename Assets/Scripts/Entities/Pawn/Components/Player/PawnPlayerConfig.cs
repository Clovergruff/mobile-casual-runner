using UnityEngine;
using Gruffdev.BCS;

[CreateAssetMenu(fileName = "Player", menuName = "Data/Pawn/Player")]
public class PawnPlayerConfig : PawnComponentConfig
{
	public override void ConstructSystemComponent(Pawn entityObject)
	{
		entityObject.AddPlayer(this);
	}
}
