using UnityEngine;
using Gruffdev.BCS;

[CreateAssetMenu(fileName = "Animation", menuName = "Data/Pickup/Animation")]
public class PickupAnimationConfig : PickupComponentConfig
{
	public float speed = 1;
	public float amplitude = 0.1f;
	public float worldZScale = 1;
	
	public override void ConstructSystemComponent(Pickup entityObject)
	{
		entityObject.AddAnimation(this);
	}
}
