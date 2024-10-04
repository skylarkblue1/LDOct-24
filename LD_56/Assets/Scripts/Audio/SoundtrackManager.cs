using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundtrackManager : MonoBehaviour
{
    public SplitSong_SO CurrentSong;

    AudioSource audioSource;
    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        PlaySplitSong();
    }

    public void PlaySplitSong() {
        audioSource.Stop();
        audioSource.PlayOneShot(CurrentSong.intro);
        StartCoroutine(PlayLoopable());
    }

    private IEnumerator PlayLoopable() {
        yield return new WaitForSeconds(CurrentSong.intro.length);
        audioSource.clip = CurrentSong.loopable;
        audioSource.loop = true;
        audioSource.Play();
    }
}
