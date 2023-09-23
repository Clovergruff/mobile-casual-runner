using UnityEngine;
using Gruffdev.BCS;
using System;

[AddComponentMenu("Pawn/Events")]
public class PawnEventsSystem : PawnSystem<PawnEventsConfig>
{
	public Action<int, Vector3, Vector3, Vector3> onTakeDamage = (amount, point, normal, velocity) => {};
	public Action<int> onSetHealth = health => {};
	public Action<Pawn> onDeath = x => {};

	public Action onHeal = () => {};
	public Action onJump = () => {};

	public Action onFootstepLeft = () => {};
	public Action onFootstepRight = () => {};
	
	public Action onStartMoving = () => {};
	public Action onStopMoving = () => {};

	public Action<Vector3> onGrounded = x => {};
	public Action onUngrounded = () => {};
	
	public Action<PawnSkinInstance> onSkinApplied = x => {};


	public override void Init(Pawn pawn, PawnEventsConfig config)
	{
		base.Init(pawn, config);
	}
}
