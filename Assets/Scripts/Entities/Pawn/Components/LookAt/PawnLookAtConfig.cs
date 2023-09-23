using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "LookAt", menuName = "Data/Pawn/LookAt")]
public class PawnLookAtConfig : PawnComponentConfig
{
	public override void ConstructSystemComponent(Pawn stackObject)
	{
		stackObject.AddLookAt(this);
	}
}
