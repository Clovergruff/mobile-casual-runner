using UnityEngine;
using Gruffdev.BCS;

[CreateAssetMenu(fileName = "Body", menuName = "Data/Pawn/Body")]
public class PawnBodyConfig : PawnComponentConfig
{
	public float mass = 10;
	public bool isKinematic = false;
	public bool useGravity = true;

	public RigidbodyInterpolation interpolation = RigidbodyInterpolation.Interpolate;
	public CollisionDetectionMode collisionDetection = CollisionDetectionMode.Continuous;

	public AxisConstraints constraints;

	public override void ConstructSystemComponent(Pawn entityObject)
	{
		entityObject.AddBody(this);
	}

	[System.Serializable]
	public class AxisConstraints
	{
		public bool freezePositionX = false;
		public bool freezePositionY = false;
		public bool freezePositionZ = false;

		public bool freezeRotationX = true;
		public bool freezeRotationY = true;
		public bool freezeRotationZ = true;
	}
}
