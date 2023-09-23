using UnityEngine;
using Gruffdev.BCS;

public partial class Pawn : MonoBehaviour, IEntity
{
	public bool hasAudio { private set; get; }
	public new PawnAudioSystem audio { private set; get; }
	public PawnAudioConfig audioConfig { private set; get; }
	
	public PawnAudioSystem AddAudio(PawnAudioConfig config)
	{
		if (hasAudio)
			Destroy(audio);
		
		audio = gameObject.AddComponent<PawnAudioSystem>();
		audioConfig = config;
		audio.Init(this, config);
		hasAudio = true;
		return audio;
	}
	
	public void RemoveAudio()
	{
		if (!hasAudio)
			return;
	
		audio.Remove();
		Destroy(audio);
	
		hasAudio = false;
		audio = null;
		audioConfig = null;
	}
}
