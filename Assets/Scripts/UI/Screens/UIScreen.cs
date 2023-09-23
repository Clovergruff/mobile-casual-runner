using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreen : MonoBehaviour
{
	public bool isOpen {get; private set;}

	public virtual void Close()
	{
		if (!isOpen)

		isOpen = false;
		gameObject.SetActive(false);
	}

	public virtual void Open()
	{
		if (isOpen)
			return;

		isOpen = true;
		gameObject.SetActive(true);
	}
}
