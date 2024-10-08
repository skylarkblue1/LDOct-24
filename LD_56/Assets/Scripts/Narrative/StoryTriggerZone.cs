using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryTriggerZone : MonoBehaviour
{
    [SerializeField]
    private StoryText_SO storyText;
    [SerializeField]
    private NarrativeTextController narrativeBox;
    [SerializeField]
    [Tooltip("How long the text stays up before it disappears")]
    private float lifespan;

    [Tooltip("Can this text pop up again?")]
    public bool Reactivateable;

    private bool isActivated = false;


    // Start is called before the first frame update
   private void OnEnable() {
        isActivated = false;
   }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player") && !isActivated) {
            TriggerStory();
        }
    }

    public void TriggerStory() {
        isActivated = true;
        StartCoroutine(ProcessText());
        SendMessageUpwards("ToggleZones", this, SendMessageOptions.DontRequireReceiver);
    }

    private IEnumerator ProcessText() {
        PauseManager.Instance.PauseForNarrative();
        string currentText = "";
        foreach (string text in storyText.texts)
        {
            currentText = text;
            narrativeBox.DisplayText(text);
            yield return new WaitForSeconds(lifespan);
        }
        yield return null;
        StartCoroutine(ProcessLifespan(currentText));
    }

    private IEnumerator ProcessLifespan(string text) {
        yield return null;
        PauseManager.Instance.ResumeForNarrative();
        narrativeBox.TryDisableTextBox(text);
        if (Reactivateable) {
            isActivated = false;
        } else  {
            gameObject.SetActive(false);
        }
    }
}