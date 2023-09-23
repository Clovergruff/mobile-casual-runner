using UnityEngine;
using Gruffdev.BCS;

public partial class Pawn : MonoBehaviour, IEntity
{
	public bool hasBody { private set; get; }
	public PawnBodySystem body { private set; get; }
	public PawnBodyConfig bodyConfig { private set; get; }
	
	public PawnBodySystem AddBody(PawnBodyConfig config)
	{
		if (hasBody)
			Destroy(body);
		
		body = gameObject.AddComponent<PawnBodySystem>();
		bodyConfig = config;
		body.Init(this, config);
		hasBody = true;
		return body;
	}
	
	public void RemoveBody()
	{
		if (!hasBody)
			return;
	
		body.Remove();
		Destroy(body);
	
		hasBody = false;
		body = null;
		bodyConfig = null;
	}
}
