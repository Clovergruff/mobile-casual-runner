using UnityEngine;
using Gruffdev.BCS;

public partial class Pawn : MonoBehaviour, IEntity
{
	public bool hasControls { private set; get; }
	public PawnControlsSystem controls { private set; get; }
	public PawnControlsConfig controlsConfig { private set; get; }
	
	public PawnControlsSystem AddControls(PawnControlsConfig config)
	{
		if (hasControls)
			Destroy(controls);
		
		controls = gameObject.AddComponent<PawnControlsSystem>();
		controlsConfig = config;
		controls.Init(this, config);
		hasControls = true;
		return controls;
	}
	
	public void RemoveControls()
	{
		if (!hasControls)
			return;
	
		controls.Remove();
		Destroy(controls);
	
		hasControls = false;
		controls = null;
		controlsConfig = null;
	}
}
