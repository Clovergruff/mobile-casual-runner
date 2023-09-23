using UnityEngine;
using Gruffdev.BCS;

[CreateAssetMenu(fileName = "Audio", menuName = "Data/Pawn/Audio")]
public class PawnAudioConfig : PawnComponentConfig
{
	public override void ConstructSystemComponent(Pawn entityObject)
	{
		entityObject.AddAudio(this);
	}
}
