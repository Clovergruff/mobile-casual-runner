using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Physics", menuName = "Data/Pawn/Physics")]
public class PawnPhysicsConfig : PawnComponentConfig
{
	public float jumpForce = 5;
	public PawnPhysicsDataProfile groundedValues;
	public PawnPhysicsDataProfile midAirValues;

	public override void ConstructSystemComponent(Pawn stackObject)
	{
		stackObject.AddPhysics(this);
	}

	[System.Serializable]
	public class PawnPhysicsDataProfile
	{
		public float acceleration = 50;
		public float damping = 10;
		public float maxHorizontalSpeed = 10;

		[Space]
		public float rotationSpeed = 10;
		public float rotationMaxSpeed = 2;
		
		[Space]
		public float lookatSpeed = 25;
	}
}
