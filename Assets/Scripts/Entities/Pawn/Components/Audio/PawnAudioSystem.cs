using UnityEngine;
using Gruffdev.BCS;

[AddComponentMenu("Pawn/Audio")]
public class PawnAudioSystem : PawnSystem<PawnAudioConfig>
{
	private PawnAudiobox _audioBox;

	public override void Init(Pawn pawn, PawnAudioConfig config)
	{
		base.Init(pawn, config);
	}

	public override void LateSetup()
	{
		_audioBox = pawn.graphics.skinInstance.audioBox;
	}

	public void PlayDamageHit()
	{
		var source = _audioBox.sources.damageHits;
		source.clip = _audioBox.clips.damageHits.GetClip();
		source.volume = Random.Range(0.8f, 1f);
		source.pitch = Random.Range(0.8f, 1.2f);
		source.Play();
	}

	public void PlayDeath()
	{
		var source = _audioBox.sources.damageHits;
		source.clip = _audioBox.clips.death.GetClip();
		source.volume = Random.Range(0.8f, 1f);
		source.pitch = Random.Range(0.9f, 1.1f);
		source.Play();
	}

	public void PlayFootstep(float speedPercentage)
	{
		var source = _audioBox.sources.footsteps;
		source.clip = _audioBox.clips.footsteps.GetClip();
		source.volume = Random.Range(0.5f, 0.6f) * speedPercentage;
		source.pitch = Random.Range(0.9f, 1.1f);
		source.Play();
	}
}
