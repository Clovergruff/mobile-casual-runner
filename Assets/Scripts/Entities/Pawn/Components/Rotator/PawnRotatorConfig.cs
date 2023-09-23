using UnityEngine;

[CreateAssetMenu(fileName = "Rotator", menuName = "Data/Pawn/Rotator")]
public class PawnRotatorConfig : PawnComponentConfig
{
	public enum RotationModeType
	{
		None = 0,
		Continuous = 1,
		Incremental = 2,
	}

	public RotationModeType rotationMode = RotationModeType.Continuous;
	[Range(0, 180)]
	public float incrementSize = 60;
	public float defaultSpeed = 10;
	public float defaultMaxSpeed = 2;

	public override void ConstructSystemComponent(Pawn entityObject)
	{
		entityObject.AddRotator(this);
	}
}
