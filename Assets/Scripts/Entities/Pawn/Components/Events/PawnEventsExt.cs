using UnityEngine;
using Gruffdev.BCS;

public partial class Pawn : MonoBehaviour, IEntity
{
	public bool hasEvents { private set; get; }
	public PawnEventsSystem events { private set; get; }
	public PawnEventsConfig eventsConfig { private set; get; }
	
	public PawnEventsSystem AddEvents(PawnEventsConfig config)
	{
		if (hasEvents)
			Destroy(events);
		
		events = gameObject.AddComponent<PawnEventsSystem>();
		eventsConfig = config;
		events.Init(this, config);
		hasEvents = true;
		return events;
	}
	
	public void RemoveEvents()
	{
		if (!hasEvents)
			return;
	
		events.Remove();
		Destroy(events);
	
		hasEvents = false;
		events = null;
		eventsConfig = null;
	}
}
