using UnityEngine;
using Gruffdev.BCS;

public partial class Pickup : MonoBehaviour, IEntity
{
	public bool hasEvents { private set; get; }
	public PickupEventsSystem events { private set; get; }
	public PickupEventsConfig eventsConfig { private set; get; }
	
	public PickupEventsSystem AddEvents(PickupEventsConfig config)
	{
		if (hasEvents)
			Destroy(events);
		
		events = gameObject.AddComponent<PickupEventsSystem>();
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
