using UnityEngine;
using Gruffdev.BCS;

[AddComponentMenu("Pickup/Pickup")]
public partial class Pickup : MonoBehaviour
	, IEntity
	, IEntityUpdate
	, IEntityFixedUpdate
	, IEntityLateUpdate
{
	public PickupConfig config;
	public IEntitySystem[] allSystems;
	public IUpdate[] updateSystems;
	public ILateUpdate[] lateUpdateSystems;
	public IFixedUpdate[] fixedUpdateSystems;

	public void Init(PickupConfig config)
	{
		this.config = config;
	}

	public void FindSystems()
	{
		allSystems = gameObject.GetComponents<IEntitySystem>();
		updateSystems = gameObject.GetComponents<IUpdate>();
		lateUpdateSystems = gameObject.GetComponents<ILateUpdate>();
		fixedUpdateSystems = gameObject.GetComponents<IFixedUpdate>();
	}

	protected virtual void Awake() => PickupEntityManager.I.AddEntity(this);
	protected virtual void OnEnable() => PickupEntityManager.I.EnableEntity(this);
	protected virtual void OnDisable() => PickupEntityManager.I.DisableEntity(this);
	protected virtual void OnDestroy() => PickupEntityManager.I.RemoveEntity(this);

	public void OnUpdate()
	{
		for (int i = 0; i < updateSystems.Length; i++)
			updateSystems[i].OnUpdate();
	}

	public void OnLateUpdate()
	{
		for (int i = 0; i < lateUpdateSystems.Length; i++)
			lateUpdateSystems[i].OnLateUpdate();
	}

	public void OnFixedUpdate()
	{
		for (int i = 0; i < fixedUpdateSystems.Length; i++)
			fixedUpdateSystems[i].OnFixedUpdate();
	}
}
