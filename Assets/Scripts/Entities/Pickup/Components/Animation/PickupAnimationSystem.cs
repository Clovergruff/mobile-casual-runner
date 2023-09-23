using UnityEngine;
using Gruffdev.BCS;

[AddComponentMenu("Pickup/Animation")]
public class PickupAnimationSystem : PickupSystem<PickupAnimationConfig>
	, IUpdate
{
	private Vector3 _skinLocalPosition = Vector3.zero;

	public override void Init(Pickup pickup, PickupAnimationConfig config)
	{
		base.Init(pickup, config);
	}

	public void OnUpdate()
	{
		_skinLocalPosition.y = Mathf.Sin(transform.position.z * config.worldZScale + Time.time * config.speed) * config.amplitude;
		pickup.graphics.skinInstance.graphicsHolder.localPosition = _skinLocalPosition;
	}
}
