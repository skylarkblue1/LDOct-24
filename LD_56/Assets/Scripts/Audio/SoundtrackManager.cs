using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundtrackManager : MonoBehaviour
{
    static public SoundtrackManager Instance {get; private set;}

    [SerializeField]
    private SplitSong_SO CurrentSong;

    AudioSource audioSource;
    private void Awake() {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 

        audioSource = GetComponent<AudioSource>();
        PlaySplitSong();
    }

    public void SetCurrentSong(SplitSong_SO splitSong) {
        // Can make the transition between songs smoother here
        audioSource.Stop();

        CurrentSong = splitSong;
    }

    public void PlaySplitSong() {
        audioSource.Stop();
        if (CurrentSong.Intro != null) {
            audioSource.PlayOneShot(CurrentSong.Intro);
        }
        StartCoroutine(PlayLoopable());
    }

    private IEnumerator PlayLoopable() {
        float delay = (CurrentSong.Intro != null ? CurrentSong.Intro.length : 0f);
        yield return new WaitForSeconds(delay);
        audioSource.clip = CurrentSong.Loopable;
        audioSource.loop = true;
        audioSource.Play();
    }
}
