using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private GameObject settingsPopup;

    private bool hasBeenToggled;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !hasBeenToggled)
        {
            hasBeenToggled = true;
            bool togglePauseBool = !pauseMenu.activeSelf;
            if (settingsPopup.activeSelf && !togglePauseBool)
            {
                settingsPopup.SetActive(false);
                return;
            }

            pauseMenu.SetActive(togglePauseBool);

            if (togglePauseBool)
            {
                PauseManager.Instance.Pause();
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                PauseManager.Instance.Resume();
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Escape))
            hasBeenToggled = false;
    }

    public void OnSettingsButton()
    {
        settingsPopup.SetActive(true);
    }

    public void OnSettingsClose()
    {
        settingsPopup.SetActive(false);
    }

    public void OnResumeButton()
    {
        pauseMenu.SetActive(false);
        PauseManager.Instance.Resume();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void onMainMenuButton()
    {
        OnResumeButton();
        SceneManager.LoadScene(0);
    }
}
