using UnityEngine;

public class PawnAudiobox : MonoBehaviour
{
	public PawnAudioClips clips;
	public PawnAudioSources sources;

	[System.Serializable]
	public struct PawnAudioClips
	{
		public AudioClipSet damageHits;
		public AudioClipSet death;
		public AudioClipSet footsteps;
	}

	[System.Serializable]
	public struct PawnAudioSources
	{
		public AudioSource damageHits;
		public AudioSource footsteps;
	}
}
