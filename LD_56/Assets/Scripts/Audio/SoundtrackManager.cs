using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundtrackManager : MonoBehaviour
{
    static public SoundtrackManager Instance {get; private set;}

    [SerializeField]
    private SplitSong_SO CurrentSong;

    // We need 2 audio sources to prevent lag from load times.
    [SerializeField]
    private AudioSource introSource;

    [SerializeField]
    private AudioSource loopSource;

    private float maxDistance;

    private Boolean playingIntro;
    
    private void Awake() {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        }

        introSource.clip = CurrentSong.Intro;
        loopSource.clip = CurrentSong.Loopable;

        PlaySplitSong();
    }

    public void SetCurrentSong(SplitSong_SO splitSong) {
        // Can make the transition between songs smoother here
        introSource.Stop();
        loopSource.Stop();
        CurrentSong = splitSong;
        introSource.clip = splitSong.Intro;
        loopSource.clip = splitSong.Loopable;
        introSource.time = 0;
        loopSource.time = 0;
        PlaySplitSong();
        // TODO function
    }

    // Song needs to be muted in case player pauses while loop is already cued.
    public void PauseCurrentSong()
    {
        if (isIntroPlaying())
            introSource.Pause();
        else
            loopSource.Pause();

        introSource.mute = true;
        loopSource.mute = true;
    }

    public void UnpauseCurrentSong()
    {
        
        
        if (isIntroPlaying())
            introSource.UnPause();
        else
            loopSource.UnPause();

        introSource.mute = false;
        loopSource.mute = false;
    }

    // Double check that player isn't about to get earraped, then return result.
    public Boolean isIntroPlaying()
    {
        if (loopSource.isPlaying && playingIntro)
        {
            introSource.Stop();
            playingIntro = false;
        }
        return playingIntro;
    }

    // Play intro if there is one, otherwise play loop.
    public void PlaySplitSong() {
        playingIntro = false;
        introSource.Stop();
        loopSource.Stop();
        if (CurrentSong.Intro != null) {
            introSource.Play();
            playingIntro = true;
            StartCoroutine(LoopHandler());
        } else
        {
            loopSource.Play();
        }

    }

    private IEnumerator LoopHandler()
    {
        for (;;)
        {
            // Wait til intro has less than 1 second remaining then preload next song.
            if (introSource.time >= CurrentSong.Intro.length - 1)
            {
                loopSource.PlayDelayed(CurrentSong.Intro.length - introSource.time);
                break;
            }

            yield return null;
        }
    }
}
