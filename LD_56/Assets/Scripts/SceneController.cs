using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; }
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
    
    public void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    public void DelayLoadScene(float delay, int buildIndex)
    {
        StartCoroutine(AddDelay(delay, buildIndex));
    }
    
    private IEnumerator AddDelay(float delay, int buildIndex) {
        yield return new WaitForSeconds(delay);
        LoadScene(buildIndex);
    }

    public void Quit() {
        Application.Quit();
    }

}
