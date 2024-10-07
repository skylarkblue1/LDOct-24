using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public GameObject mainMenu;

    public GameObject settingPopup;

    public GameObject creditsPopup;

    private CanvasGroup menuCanvas;
    private FadeUI menuFadeScript;

    private void Start()
    {
        menuCanvas = mainMenu.GetComponent<CanvasGroup>();
        menuFadeScript = mainMenu.GetComponent<FadeUI>();
    }

    public void OnStartButton()
    {
        menuFadeScript.toggleFadeOut = true;
        StartCoroutine(WaitForFade());
    }

    IEnumerator WaitForFade()
    {
        bool canvasAlpha = menuCanvas.alpha <= 0;
        yield return new WaitUntil(isAlphaReady);
        SceneManager.LoadScene(1);
    }

    bool isAlphaReady()
    {
        return menuCanvas.alpha <= 0;
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
