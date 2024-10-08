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
            musicSlider.value = audioSettings.MusicVolume;
            musicSlider.minValue = 0f;
            musicSlider.maxValue = 1f;
            musicSlider.onValueChanged.AddListener(UpdateMusicVolume);
        }

        if (sfxSlider) {
            sfxSlider.value = audioSettings.SfxVolume;
            sfxSlider.minValue = 0f;
            sfxSlider.maxValue = 1f;
            sfxSlider.onValueChanged.AddListener(UpdateSfxVolume);
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

    public void UpdateMusicVolume(System.Single vol) {
        audioSettings.MusicVolume = vol;
        SetMusicVolume();
    }

    public void UpdateSfxVolume(System.Single vol) {
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
