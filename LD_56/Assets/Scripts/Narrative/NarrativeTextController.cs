using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NarrativeTextController : MonoBehaviour
{
    private GameObject textBox;
    private TextMeshProUGUI tmp;

    private void Awake() {
        textBox = transform.parent.gameObject;
        textBox.SetActive(false);

        tmp = GetComponent<TextMeshProUGUI>();
        tmp.text = "";
    }

    public void DisplayText(string text) {
        tmp.text = text;
        textBox.SetActive(true);
    }

    // The StoryTriggerZone will try and disable the textBox, if the text box still has the same text up on the trigger zone
    public void TryDisableTextBox(string triggerZoneText) {
        if (triggerZoneText == tmp.text) {
            textBox.SetActive(false);
        }
    }
}