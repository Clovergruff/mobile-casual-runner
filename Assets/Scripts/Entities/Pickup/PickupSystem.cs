using UnityEngine;
using Gruffdev.BCS;

public class PickupSystem<T> : MonoBehaviour, IEntitySystem
	where T : PickupComponentConfig
{
	public Pickup pickup { get; private set; }

	[SerializeField] protected T config;

	public T Config { get => config; }

	public virtual void Init(Pickup pickup, T config)
	{
		this.config = config;
		this.pickup = pickup;
	}

	public virtual void Init() { }
	public virtual void LateSetup() { }
	public virtual void Remove() { }
	public virtual void ReusedSetup() { }
}
