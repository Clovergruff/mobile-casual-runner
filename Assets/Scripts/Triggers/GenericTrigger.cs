using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GenericTrigger : Trigger
{
    [Space]
	public GenericTriggerEvents events = new GenericTriggerEvents();

	protected override void OnEnter(PawnTriggerDetectorSystem detector)
	{
		events.detectorEnter.Invoke(detector);
		events.onEnter.Invoke();
	}

	protected override void OnExit(PawnTriggerDetectorSystem detector)
	{
		events.detectorExit.Invoke(detector);
		events.onExit.Invoke();
	}

	[System.Serializable]
	public class GenericTriggerEvents
	{
		public UnityEvent onEnter = new UnityEvent();
		public UnityEvent onExit = new UnityEvent();

		public UnityEvent<PawnTriggerDetectorSystem> detectorEnter {get; private set;} = new UnityEvent<PawnTriggerDetectorSystem>();
		public UnityEvent<PawnTriggerDetectorSystem> detectorExit {get; private set;} = new UnityEvent<PawnTriggerDetectorSystem>();
	}
}
