using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioSetting", menuName = "Custom/AudioSettings", order = 1)]
public class AudioSO : ScriptableObject
{
    [Range(0f, 1f)]
    public float MusicVolume;
    [Range(0f, 1f)]
    public float SfxVolume;

}
