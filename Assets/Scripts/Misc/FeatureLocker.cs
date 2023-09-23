using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeauRoutine;

public class FeatureLocker
{
	public bool locked { get; private set; }
	public float time { get; private set; }

	private MonoBehaviour behaviour;
	private Routine lockCoroutine = Routine.Null;

	public void Init(MonoBehaviour behaviour)
	{
		this.behaviour = behaviour;
	}

	public void Lock()
	{
		time = 0;
		locked = true;
	}

    public void Lock(float time)
	{
		if (time <= 0)
			return;

		if (time > this.time)
			this.time = time;

		if (!locked)
		{
			lockCoroutine = Routine.Start(behaviour, LockSequence());
			locked = true;
		}
	}

	public void Unlock()
	{
		StopLockCoroutine();
		locked = false;
		time = 0;
	}

	private void StopLockCoroutine()
	{
		if (lockCoroutine != null)
		{
			lockCoroutine.Stop();
			lockCoroutine = Routine.Null;
		}
	}

	private IEnumerator LockSequence()
    {
		while (time > 0)
		{
        	time -= Time.deltaTime;
			yield return null;
		}

		Unlock();
    }
}
