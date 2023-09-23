using UnityEngine;
using Gruffdev.BCS;

[CreateAssetMenu(fileName = "Events", menuName = "Data/Pawn/Events")]
public class PawnEventsConfig : PawnComponentConfig
{
	public override void ConstructSystemComponent(Pawn entityObject)
	{
		entityObject.AddEvents(this);
	}
}
