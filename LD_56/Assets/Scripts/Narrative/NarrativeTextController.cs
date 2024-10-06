using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NarrativeTextController : MonoBehaviour
{
    private TextMeshProUGUI textBox;

    private void Awake() {
        textBox = GetComponent<TextMeshProUGUI>();
        textBox.text = "";
        textBox.enabled = false;     
    }

    public void DisplayText(string text) {
        textBox.text = text;
        textBox.enabled = true;
    }

    // The StoryTriggerZone will try and disable the textBox, if the text box still has the same text up on the trigger zone
    public void TryDisableTextBox(string triggerZoneText) {
        if (triggerZoneText == textBox.text) {
            textBox.enabled = false;
        }
    }
}