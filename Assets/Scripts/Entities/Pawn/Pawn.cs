using UnityEngine;
using Gruffdev.BCS;

[AddComponentMenu("Pawn/Pawn")]
public partial class Pawn : MonoBehaviour
	, IEntity
	, IEntityUpdate
	, IEntityFixedUpdate
	, IEntityLateUpdate
{
	public PawnConfig config;
	public IEntitySystem[] allSystems;
	public IUpdate[] updateSystems;
	public ILateUpdate[] lateUpdateSystems;
	public IFixedUpdate[] fixedUpdateSystems;

	public void Init(PawnConfig config)
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

	protected virtual void Awake() => PawnEntityManager.I.AddEntity(this);
	protected virtual void OnEnable() => PawnEntityManager.I.EnableEntity(this);
	protected virtual void OnDisable() => PawnEntityManager.I.DisableEntity(this);
	protected virtual void OnDestroy() => PawnEntityManager.I.RemoveEntity(this);

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
