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
    [SerializeField]
    [Tooltip("Can this text pop up again?")]
    private bool reactivateable;

    private bool isActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player") && !isActivated) {
            isActivated = true;
            narrativeBox.DisplayText(narrativeText);
            StartCoroutine(ProcessLifespan());
        }
    }

    private IEnumerator ProcessLifespan() {
        yield return new WaitForSeconds(lifespan);
        narrativeBox.TryDisableTextBox(narrativeText);
        if (reactivateable) {
            isActivated = false;
        } else  {
            gameObject.SetActive(false);
        }
    }
}
