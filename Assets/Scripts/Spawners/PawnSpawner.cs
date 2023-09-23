using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gruffdev.BCS;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PawnSpawner : Spawner
{
	[SerializeField] private PawnConfig pawnData;
	
	public override void Spawn() => SpawnPawn();

	public Pawn SpawnPawn()
	{
		if (pawnData)
		{
			var pawnInstance = PawnFactory.Create(pawnData, transform.position, transform.rotation);
			return pawnInstance;
		}

		return null;
	}
}