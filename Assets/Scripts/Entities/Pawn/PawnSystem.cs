using UnityEngine;
using Gruffdev.BCS;

public class PawnSystem<T> : MonoBehaviour, IEntitySystem
	where T : PawnComponentConfig
{
	public Pawn pawn { get; private set; }

	[SerializeField] protected T config;

	public T Config { get => config; }

	public virtual void Init(Pawn pawn, T config)
	{
		this.config = config;
		this.pawn = pawn;
	}

	public virtual void Init() { }
	public virtual void LateSetup() { }
	public virtual void Remove() { }
	public virtual void ReusedSetup() { }
}
