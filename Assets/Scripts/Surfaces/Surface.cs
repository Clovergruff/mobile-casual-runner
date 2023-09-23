using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SurfaceMaterial;

public class Surface : MonoBehaviour
{
	public SurfaceMaterial material;

	[Header("Physics")]
	public new Rigidbody rigidbody;

	[Header("Feedback")]
	public AudioSource collisionAudioSource;

	private float allowedCollisionFeedbackTime;

	private bool collisionFeedbackPossible = true;

	private void Awake()
	{
		if (!collisionAudioSource)
		{
			collisionFeedbackPossible = false;
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (!collisionFeedbackPossible)
			return;

		float impulse = collision.impulse.magnitude;

		if (Time.time > allowedCollisionFeedbackTime && material.Properties.collisionFeedbacks.Length > 0 && impulse > 0.5f)
		{
			CollisionFeedbackData feedbackData = null;
			float minDist = Mathf.Infinity;

			foreach (var fb in material.Properties.collisionFeedbacks)
			{
				float forceDist = Mathf.Abs(fb.minForce - impulse);
				if (forceDist < minDist)
				{
					feedbackData = fb;
					minDist = forceDist;
				}
			}

			// Execute the feedback data
			float power = Mathf.InverseLerp(0, 500, impulse);

			// if (collisionAudioSource)
			// {
			// 	collisionAudioSource.volume = power;
			// 	collisionAudioSource.PlayOneShot(feedbackData.collisionSound.GetClip());
			// }

			allowedCollisionFeedbackTime = Time.time + 0.1f;
		}
	}
}
