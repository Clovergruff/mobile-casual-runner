using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioClipSet", menuName = "Data/Audio/Clip Set", order = 0)]
public class AudioClipSet : ScriptableObject
{
    public AudioClip[] clips;

    public AudioClip GetClip() => clips.GetRandomOrDefault();
}
