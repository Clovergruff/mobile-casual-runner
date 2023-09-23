using UnityEngine;

public static class AnimHash
{
	public static readonly int LOCAL_SPEED_X = Animator.StringToHash("LocalSpeedX");
	public static readonly int LOCAL_SPEED_Y = Animator.StringToHash("LocalSpeedY");
	public static readonly int LOCAL_SPEED_Z = Animator.StringToHash("LocalSpeedZ");

	public static readonly int LAND = Animator.StringToHash("Land");
	public static readonly int LAND_ID = Animator.StringToHash("LandId");

	public static readonly int JUMP = Animator.StringToHash("Jump");
	public static readonly int JUMP_ID = Animator.StringToHash("JumpId");

	public static readonly int MOVING = Animator.StringToHash("Moving");
	public static readonly int MOVING_START = Animator.StringToHash("MovingStart");
	public static readonly int MOVING_END = Animator.StringToHash("MovingEnd");
	public static readonly int GROUNDED = Animator.StringToHash("Grounded");

	public static readonly int CELEBRATE = Animator.StringToHash("Celebrate");
	public static readonly int CELEBRATE_ID = Animator.StringToHash("CelebrateId");
}
