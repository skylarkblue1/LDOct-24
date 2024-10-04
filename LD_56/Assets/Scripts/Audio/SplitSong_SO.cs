using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Split Song", menuName = "ScriptableObjects/Audio", order = 1)]
public class SplitSong_SO : ScriptableObject
{
    [Tooltip("This audio clip will only be played once")]
    public AudioClip intro;

    [Tooltip("This audio clip will loop")]
    public AudioClip loopable;
}
