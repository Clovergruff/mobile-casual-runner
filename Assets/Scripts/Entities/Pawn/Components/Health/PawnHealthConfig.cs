using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Health", menuName = "Data/Pawn/Health")]
public class PawnHealthConfig : PawnComponentConfig
{
	[Range(1, 255)]
	public int maxHealth = 3;
	public int defaultHealth = 3;

	public override void ConstructSystemComponent(Pawn entityObject)
	{
		entityObject.AddHealth(this);
	}
}
