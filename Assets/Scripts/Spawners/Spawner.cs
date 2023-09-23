using System.Collections;
using System.Collections.Generic;
using Gruffdev.BCS;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
	public enum ExecutionType
	{
		Awake = 0,
		Start = 1,
		Enable = 2,
		Disable = 3,
		Destroy = 4,
		Manual = 5,
	}

	[SerializeField, InspectorName("Spawn on")] private ExecutionType executeOn;
	[SerializeField] private new Optional<Renderer> renderer;

	protected virtual void Awake()
	{
		if (renderer.enabled)
			renderer.value.enabled = false;

		TrySpawnOn(ExecutionType.Awake);
	}
	protected virtual void Start() => TrySpawnOn(ExecutionType.Start);
	private void OnEnable() => TrySpawnOn(ExecutionType.Enable);
	private void OnDisable() => TrySpawnOn(ExecutionType.Disable);
	private void OnDestroy() => TrySpawnOn(ExecutionType.Destroy);

	private void TrySpawnOn(ExecutionType executionType)
	{
		if (executionType == executeOn)
			Spawn();
	}

	public abstract void Spawn();
}