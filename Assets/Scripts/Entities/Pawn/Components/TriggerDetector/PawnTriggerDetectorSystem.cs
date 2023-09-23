using UnityEngine;
using Gruffdev.BCS;
using System.Collections.Generic;
using System.Linq;

[AddComponentMenu("Pawn/TriggerDetector")]
public class PawnTriggerDetectorSystem : PawnSystem<PawnTriggerDetectorConfig>
{
	public override void Init(Pawn pawn, PawnTriggerDetectorConfig config)
	{
		base.Init(pawn, config);
	}

	private void OnTriggerEnter(Collider other)
	{
		(List<ITriggerable> triggerables, bool triggerablesFound) = GetTriggerables(other);
		if (triggerablesFound)	triggerables.ForEach(t => t.Enter(this));
	}

	private void OnTriggerExit(Collider other)
	{
		(List<ITriggerable> triggerables, bool triggerablesFound) = GetTriggerables(other);
		if (triggerablesFound)	triggerables.ForEach(t => t.Exit(this));
	}

	private (List<ITriggerable> triggerables, bool triggerablesFound) GetTriggerables(Collider coll)
	{
		var triggerables = coll.GetComponents<ITriggerable>()
			.Where(x => x.CompatiblePawns.HasFlag(pawn.config.type)).ToList();
		
		return (triggerables, triggerables.Count > 0);
	}
}
