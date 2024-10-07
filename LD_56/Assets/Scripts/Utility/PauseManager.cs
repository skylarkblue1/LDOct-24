using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set;}
    private void Awake() {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    public void Pause() {
        Time.timeScale = 0f;
        // Pause Audio - Music
    }

    public void Resume() {
        Debug.Log("Resuming Game");
        Time.timeScale = 1f;
        // Resume Audio - Music
    }

    public void DelayedResume(float delay) {
        StartCoroutine(DelayedResumeCoroutine(delay));
    }

    private IEnumerator DelayedResumeCoroutine(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Resume();
    }
}
