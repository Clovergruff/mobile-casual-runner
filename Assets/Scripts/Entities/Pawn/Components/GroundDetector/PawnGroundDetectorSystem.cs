using UnityEngine;
using Gruffdev.BCS;

[AddComponentMenu("Pawn/GroundDetector")]
public class PawnGroundDetectorSystem : PawnSystem<PawnGroundDetectorConfig>
	, IFixedUpdate
{
	public bool isGrounded, isSliding;
	public Vector3 groundPoint, groundNormal;
	public RaycastHit groundHit;
    public float groundFoundTime;

    private CommonAssetsData commonAssetsData;

	public float timeWhenGrounded			{private set; get;}
	public float timeWhenUngrounded			{private set; get;}

	private delegate void GroundDetectionUpdate();	
	GroundDetectionUpdate groundDetectionUpdate;

	public override void LateSetup()
	{
        commonAssetsData = CommonAssetsData.I;
		Enable();
	}

	public void OnFixedUpdate()
	{
		groundDetectionUpdate();
	}

	public void Enable() => groundDetectionUpdate = GroundDetection;
	public void Disable() => groundDetectionUpdate = NoGroundDetection;

	private void NoGroundDetection() {}
    private void GroundDetection()
	{
		bool groundFound = false;
		Vector3 previousVelocity = pawn.body.rigidbody.velocity;

		if (PerformGroundCheck(out groundHit))
		{
			if (previousVelocity.y < 0.1f && transform.position.y < groundHit.point.y + 0.25f)
			{
				groundPoint = groundHit.point;
				groundNormal = groundHit.normal;

				Vector3 newPosition = transform.position;
				newPosition.y = groundPoint.y;
				transform.position = newPosition;

				groundFound = true;
				groundFoundTime = Time.time;
			}
		}

		SetGrounded(groundFound, previousVelocity);
	}

	public bool PerformGroundCheck(out RaycastHit hit)
	{
		var capsuleCollider = pawn.graphics.skinInstance.collider.collider;
		float rayOffset = capsuleCollider.center.y - capsuleCollider.height * 0.5f + capsuleCollider.radius + 0.1f;
		return Physics.SphereCast(
			transform.position + new Vector3(0, rayOffset, 0),
			capsuleCollider.radius - 0.02f,
			Vector3.down,
			out hit,
			rayOffset + 1,
			commonAssetsData.layerMasks.solid);
	}

	private void SetGrounded(bool isGrounded, Vector3 previousVelocity)
	{
		if (!this.isGrounded && isGrounded)
		{
			this.isGrounded = true;
			pawn.events.onGrounded.Invoke(previousVelocity);
			timeWhenGrounded = Time.time;
		}
		else if (this.isGrounded && !isGrounded)
		{
			this.isGrounded = false;
			pawn.events.onUngrounded.Invoke();
			timeWhenUngrounded = Time.time;
		}
	}

	private void OnJump(float jumpForce)
	{
		SetGrounded(false, pawn.body.rigidbody.velocity);
	}
}
