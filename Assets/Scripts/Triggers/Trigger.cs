using System.Collections;
using System.Collections.Generic;
using Gruffdev.BCS;
using UnityEngine;
using static PawnConfig;

public abstract class Trigger : MonoBehaviour, ITriggerable
{
	[SerializeField] private PawnType compatiblePawns = PawnType.Player;
    [SerializeField] private Optional<int> maxEnterCount;
    [SerializeField] private Optional<int> maxExitCount;

    [Space]
    [SerializeField] private Optional<float> onEnterDelay = new Optional<float>(0.5f, false);
    [SerializeField] private Optional<float> onExitDelay = new Optional<float>(0.5f, false);

	public PawnType CompatiblePawns 	{ get { return compatiblePawns; } set { compatiblePawns = value; } }

    protected int enteredCount;
    protected int exitedCount;

	public void Enter(PawnTriggerDetectorSystem detector)
	{
        if (maxEnterCount.enabled && enteredCount >= maxEnterCount.value)
            return;

        enteredCount++;

        if (onEnterDelay.enabled)
            StartCoroutine(DelayedEnterSequence(onEnterDelay.value, detector));
        else
            OnEnter(detector);
	}

	public void Exit(PawnTriggerDetectorSystem detector)
	{
        if (maxExitCount.enabled && exitedCount >= maxExitCount.value)
            return;

        exitedCount++;

        if (onExitDelay.enabled)
            StartCoroutine(DelayedExitSequence(onExitDelay.value, detector));
        else
            OnExit(detector);
	}

    protected virtual void OnEnter(PawnTriggerDetectorSystem detector) {}
    protected virtual void OnExit(PawnTriggerDetectorSystem detector) {}

    private IEnumerator DelayedEnterSequence(float delay, PawnTriggerDetectorSystem detector)
    {
        yield return new WaitForSeconds(delay);
        OnEnter(detector);
    }

    private IEnumerator DelayedExitSequence(float delay, PawnTriggerDetectorSystem detector)
    {
        yield return new WaitForSeconds(delay);
        OnEnter(detector);
    }
}
