using UnityEngine;
using System.Collections;

[AddComponentMenu("Pawn/Body")]
public class PawnBodySystem : PawnSystem<PawnBodyConfig>
{
	public new Collider collider;
	public new Rigidbody rigidbody;

	public override void Init(Pawn pawn, PawnBodyConfig config)
	{
		base.Init(pawn, config);

		ConstructRigidbody();
	}

	public void OnJump(float force)
	{
		rigidbody.velocity = new Vector3(rigidbody.velocity.x, force, rigidbody.velocity.z);
	}

	private void ConstructRigidbody()
	{
		rigidbody = gameObject.AddComponent<Rigidbody>();
		rigidbody.mass = config.mass;
		rigidbody.isKinematic = config.isKinematic;
		rigidbody.useGravity = config.useGravity;
		rigidbody.drag = 0;
		rigidbody.angularDrag = 0;

		rigidbody.interpolation = config.interpolation;
		rigidbody.collisionDetectionMode = config.collisionDetection;

		if (config.constraints.freezePositionX) rigidbody.constraints |= RigidbodyConstraints.FreezePositionX;
		if (config.constraints.freezePositionY) rigidbody.constraints |= RigidbodyConstraints.FreezePositionY;
		if (config.constraints.freezePositionZ) rigidbody.constraints |= RigidbodyConstraints.FreezePositionZ;

		if (config.constraints.freezeRotationX) rigidbody.constraints |= RigidbodyConstraints.FreezeRotationX;
		if (config.constraints.freezeRotationY) rigidbody.constraints |= RigidbodyConstraints.FreezeRotationY;
		if (config.constraints.freezeRotationZ) rigidbody.constraints |= RigidbodyConstraints.FreezeRotationZ;
	}
}
