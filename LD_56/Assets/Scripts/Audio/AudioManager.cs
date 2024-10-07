using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Volume Control")]
    [SerializeField]
    AudioSO audioSettings;

    [Header("Audio Sources")]
    [SerializeField]
    GameObject jukebox;
    List<AudioSource> allSfxSources;

    [Header("Settings Slider")]
    [SerializeField]
    Slider musicSlider;
    [SerializeField]
    Slider sfxSlider;

    AudioSource[] jukeboxSources;
    private void Awake() {
        jukeboxSources = jukebox.GetComponents<AudioSource>();
        allSfxSources = GameObject.FindObjectsOfType<AudioSource>().ToList<AudioSource>();
        foreach (AudioSource audioSource in jukeboxSources)
            allSfxSources.Remove(audioSource);
        
    }

    private void Start() {
        SetMusicVolume();
        if (musicSlider) {
            // Update slider volume
            musicSlider.value = audioSettings.MusicVolume;
            // Update slider handle to proper position
            musicSlider.handleRect.anchorMin = new(audioSettings.MusicVolume,0f);
            musicSlider.handleRect.anchorMax = new(audioSettings.MusicVolume,1f);
        }

        if (sfxSlider) {
            sfxSlider.value = audioSettings.SfxVolume;
            sfxSlider.handleRect.anchorMin = new(audioSettings.SfxVolume,0f);
            sfxSlider.handleRect.anchorMax = new(audioSettings.SfxVolume,1f);
        }
    }

    public void SetMusicVolume() {
        foreach (AudioSource audioSource in jukeboxSources)
            audioSource.volume = audioSettings.MusicVolume;
    }

    public void SetSfxVolume() {
        foreach (var audSor in allSfxSources)
        {
            audSor.volume = audioSettings.SfxVolume;
        }
    }

    public void UpdateMusicVolume(float vol) {
        vol = Mathf.Clamp(vol, 0f,1f);
        audioSettings.MusicVolume = vol;
        SetMusicVolume();
    }

    public void UpdateSfxVolume(float vol) {
        vol = Mathf.Clamp(vol, 0f,1f);
        audioSettings.SfxVolume = vol;
        SetSfxVolume();
    }

    public float GetMusicVolume() {
        return audioSettings.MusicVolume;
    }

    public float GetSfxVolume() {
        return audioSettings.SfxVolume;
    }
}
