using UnityEngine;
using Gruffdev.BCS;

public partial class Pickup : MonoBehaviour, IEntity
{
	public bool hasAnimation { private set; get; }
	public PickupAnimationSystem animation { private set; get; }
	public PickupAnimationConfig animationConfig { private set; get; }
	
	public PickupAnimationSystem AddAnimation(PickupAnimationConfig config)
	{
		if (hasAnimation)
			Destroy(animation);
		
		animation = gameObject.AddComponent<PickupAnimationSystem>();
		animationConfig = config;
		animation.Init(this, config);
		hasAnimation = true;
		return animation;
	}
	
	public void RemoveAnimation()
	{
		if (!hasAnimation)
			return;
	
		animation.Remove();
		Destroy(animation);
	
		hasAnimation = false;
		animation = null;
		animationConfig = null;
	}
}
