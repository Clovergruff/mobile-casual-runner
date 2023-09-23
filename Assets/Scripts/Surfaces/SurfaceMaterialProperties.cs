using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SurfaceProperties", menuName = "Data/Surfaces/Surface Properties", order = 3)]
public partial class SurfaceMaterialProperties : ScriptableObject
{
	[Header("Audio")]
	// public AudioGroup landingSounds;
	// public AudioGroup footstepSounds;

	[Header("Rigidbody physics")]
	public CollisionFeedbackData[] collisionFeedbacks;
}
