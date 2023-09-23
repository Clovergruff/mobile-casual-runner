using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInstance : MonoBehaviour
{
	public static LevelInstance I;

	public Transform startPoint;
	public Transform endPoint;

	private void Awake()
	{
		// This object probably shouldn't be used like this, but this is just a small prototype ¯\_(ツ)_/¯
		I = this;
	}
}
