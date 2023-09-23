using UnityEngine;
using Gruffdev.BCS;

public partial class Pawn : MonoBehaviour, IEntity
{
	public bool isPlayer { private set; get; }
	public PawnPlayerSystem player { private set; get; }
	public PawnPlayerConfig playerConfig { private set; get; }
	
	public PawnPlayerSystem AddPlayer(PawnPlayerConfig config)
	{
		if (isPlayer)
			Destroy(player);
		
		player = gameObject.AddComponent<PawnPlayerSystem>();
		playerConfig = config;
		player.Init(this, config);
		isPlayer = true;
		return player;
	}
	
	public void RemovePlayer()
	{
		if (!isPlayer)
			return;
	
		player.Remove();
		Destroy(player);
	
		isPlayer = false;
		player = null;
		playerConfig = null;
	}
}
