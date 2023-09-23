using UnityEngine;
using System.Collections;
using static PawnGroundDetectorSystem;

[CreateAssetMenu(fileName = "GroundDetector", menuName = "Data/Pawn/GroundDetector")]
public class PawnGroundDetectorConfig : PawnComponentConfig
{
	public LayerMask groundLayers;
	public bool slideOffSteepSurfaces = true;
	public bool alignToGround = false;
	public bool lockToGround = false;
	[Range(0, 1)]
	public float slidingSurfaceAngle = 0.5f;

	public override void ConstructSystemComponent(Pawn entityObject)
	{
		entityObject.AddGroundDetector(this);
	}
}
