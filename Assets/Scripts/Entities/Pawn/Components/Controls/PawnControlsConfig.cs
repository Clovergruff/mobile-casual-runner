using UnityEngine;
using Gruffdev.BCS;

[CreateAssetMenu(fileName = "Controls", menuName = "Data/Pawn/Controls")]
public class PawnControlsConfig : PawnComponentConfig
{
	public override void ConstructSystemComponent(Pawn entityObject)
	{
		entityObject.AddControls(this);
	}
}
