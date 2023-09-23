using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomExt
{
	/// <summary>
	/// Sets new seed to the UnityEngine.Random.InitState, and returns old seed for restoring
	/// </summary>
	public static Random.State InitState (int seed)
	{
		var oldState = Random.state;
		Random.InitState(seed);
		return oldState;
	}
	/// <summary>
	/// Shorthand for UnityEngine.Random.state = state
	/// </summary>
	public static void RestoreState (Random.State state)
	{
		Random.state = state;
	}

	/// <summary>
	/// Sets rounded to int Time.time * 10. Returns old seed.
	/// </summary>
	public static Random.State InitStateToTime() => InitState(Mathf.RoundToInt(Time.time * 10f));
}