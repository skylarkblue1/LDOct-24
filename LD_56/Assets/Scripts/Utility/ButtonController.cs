using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{

    public GameObject settingPopup;

    public GameObject creditsPopup;

    public void OnStartButton()
    {
        SceneManager.LoadScene(1); //Make sure this goes to the game scene
    }


    public void OnSettingsButton()
    {
        settingPopup.SetActive(true);
    }
    public void OnSettingsClose()
    {
        settingPopup.SetActive(false);
    }


    public void OnCreditsButton()
    {
        creditsPopup.SetActive(true);
    }

    public void OnCreditsClose()
    {
        creditsPopup.SetActive(false);
    }


    public void OnQuitButton()
    {
        Application.Quit();
    }
}
