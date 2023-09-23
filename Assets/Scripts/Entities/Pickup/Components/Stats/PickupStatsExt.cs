using UnityEngine;
using Gruffdev.BCS;

public partial class Pickup : MonoBehaviour, IEntity
{
	public bool hasStats { private set; get; }
	public PickupStatsSystem stats { private set; get; }
	public PickupStatsConfig statsConfig { private set; get; }
	
	public PickupStatsSystem AddStats(PickupStatsConfig config)
	{
		if (hasStats)
			Destroy(stats);
		
		stats = gameObject.AddComponent<PickupStatsSystem>();
		statsConfig = config;
		stats.Init(this, config);
		hasStats = true;
		return stats;
	}
	
	public void RemoveStats()
	{
		if (!hasStats)
			return;
	
		stats.Remove();
		Destroy(stats);
	
		hasStats = false;
		stats = null;
		statsConfig = null;
	}
}
