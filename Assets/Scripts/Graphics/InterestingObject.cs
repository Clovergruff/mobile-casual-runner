using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterestingObject : MonoBehaviour
{
	public Transform lookAtTransform;

	private void Reset()
	{
		lookAtTransform = transform;
	}
}
