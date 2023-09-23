using UnityEngine;
using Gruffdev.BCS;
using BeauRoutine;
using System.Collections;

[AddComponentMenu("Pickup/Collection")]
public class PickupCollectionSystem : PickupSystem<PickupCollectionConfig>
{
	private readonly Vector3 TARGET_COLLECT_SCALE = new Vector3(0.2f, 0.2f, 0.2f);

	public bool isCollected {get; private set;}

	private Routine _collectionRoutine = Routine.Null;
	private Vector3 _collectJumpVector = Vector3.zero;

	public override void Init(Pickup pickup, PickupCollectionConfig config)
	{
		base.Init(pickup, config);
	}

	public override void LateSetup()
	{
		pickup.events.onCollected += OnCollected;
	}

	private void OnCollected(Pawn pawn)
	{
		if (isCollected)
			return;

		isCollected = true;

		_collectionRoutine.Stop();
		_collectionRoutine = Routine.Start(CollectionCoroutine(pawn, Random.Range(0.2f, 0.3f)));

		// This part is all kinds of strange and wrong, but I feel like I'm running out of time :<
		GameManager.I.AddScore(1);
	}

	private IEnumerator CollectionCoroutine(Pawn collectorPawn, float duration)
	{
		Vector3 origPosition = transform.position;
		Vector3 origScale = transform.localScale;
		float t = 0;
		
		float jumpY = Random.Range(0.2f, 0.6f);

		while (t < 1)
		{
			t += Time.deltaTime / duration;

			_collectJumpVector.y = Mathf.Sin(Mathf.Deg2Rad * 180 * t) * jumpY;

			var targetPoint = collectorPawn.graphics.skinInstance.bones.chest.position;
			transform.position = Vector3.Lerp(origPosition, targetPoint, t) + _collectJumpVector;
			transform.localScale = Vector3.Lerp(origScale, TARGET_COLLECT_SCALE, t);

			yield return null;
		}

		pickup.events.onCollectionAnimationEnd.Invoke();

		gameObject.SetActive(false);
	}
}