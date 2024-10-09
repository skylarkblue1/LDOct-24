using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set;}

    private float[] originalSpeeds;

    private bool narrativeActive;

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

    public void PauseForNarrative()
    {
        narrativeActive = true;
        setEnemiesAIEnabled(false);
        setFreezePlayer(true);
    }

    public void ResumeForNarrative()
    {
        narrativeActive = false;
        setEnemiesAIEnabled(true);
        setFreezePlayer(false);
    }

    public void Pause() {
        Time.timeScale = 0f;
        setMusicEnabled(false);
        setSfxEnabled(false);

        if (narrativeActive) return;
        setEnemiesAIEnabled(false);
        setFreezePlayer(true);
    }

    public void Resume() {
        Time.timeScale = 1f;
        setMusicEnabled(true);
        setSfxEnabled(true);

        if (narrativeActive) return;
        setEnemiesAIEnabled(true);
        setFreezePlayer(false);
    }

    public void DelayedResume(float delay) {
        StartCoroutine(DelayedResumeCoroutine(delay));
    }

    private IEnumerator DelayedResumeCoroutine(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Resume();
    }

    private void setMusicEnabled(bool enabled)
    {
        if (enabled)
            SoundtrackManager.Instance.UnpauseCurrentSong();
        else
            SoundtrackManager.Instance.PauseCurrentSong();
    }

    private void setSfxEnabled(bool enabled)
    {
        if (enabled)
            AudioManager.Instance.UnmuteSFX();
        else
            AudioManager.Instance.MuteSFX();
    }

    private void setEnemiesAIEnabled(bool enabled)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject obj in enemies)
        {
            EnemyAI ai = obj.GetComponent<EnemyAI>();
            if (ai != null)
                ai.disableAI = !enabled;
        }
    }

    private void setFreezePlayer(bool freeze)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (originalSpeeds == null || originalSpeeds.Length == 0)
            originalSpeeds = new float[players.Length];
        if (freeze && players.Length != originalSpeeds.Length)
            originalSpeeds = new float[players.Length];
        int count = 0;
        foreach (GameObject obj in players)
        {
            FirstPersonMovement move = obj.GetComponent<FirstPersonMovement>();
            if (move != null)
            {
                float ogSpeed = move.moveSpeed;
                move.moveSpeed = freeze ? 0 : originalSpeeds[count];

                originalSpeeds[count] = ogSpeed;
                count++;

            }
            PlayerAttack atk = obj.GetComponent<PlayerAttack>();
            if (atk != null)
            {
                atk.enabled = !freeze;
            }
        }
    }
}
