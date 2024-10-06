using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoryTriggerZone : MonoBehaviour
{
    [SerializeField]
    private string narrativeText;
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
            isActivated = true;
            narrativeBox.DisplayText(narrativeText);
            StartCoroutine(ProcessLifespan());
            SendMessageUpwards("ToggleZones", this, SendMessageOptions.DontRequireReceiver);
        }
    }

    private IEnumerator ProcessLifespan() {
        yield return new WaitForSeconds(lifespan);
        narrativeBox.TryDisableTextBox(narrativeText);
        if (Reactivateable) {
            isActivated = false;
        } else  {
            gameObject.SetActive(false);
        }
    }
}