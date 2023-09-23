using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

[AddComponentMenu("Pawn/Health")]
public class PawnHealthSystem : PawnSystem<PawnHealthConfig>
{
	public int health = 10;
	public bool isAlive = true;

	public override void Init(Pawn pawn, PawnHealthConfig config)
	{
		base.Init(pawn, config);

		health = config.defaultHealth;
	}

	public void TakeDamage(int amount) => RawTakeDamage(amount, transform.position, transform.up, Vector3.zero, null);
	public void TakeDamage(int amount, Vector3 point, Vector3 normal, Vector3 velocity, Pawn damagerPawn = null) => RawTakeDamage(amount, point, normal, velocity, damagerPawn);

	private void RawTakeDamage(int amount, Vector3 point, Vector3 normal, Vector3 velocity, Pawn damagerPawn)
	{
		pawn.events.onTakeDamage(amount, point, normal, velocity);
		SetHealth(health - amount);
	}

	public void SetHealth(int targetHealth)
	{
		pawn.events.onSetHealth.Invoke(targetHealth);

		health = targetHealth;
		if (health <= 0)
			Death();
	}

	private void Death()
	{
		isAlive = false;
		pawn.events.onDeath.Invoke(pawn);
		pawn.enabled = false;
		// gameObject.SetActive(false);
	}
}
