using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class ToggleStoryTriggerZone : MonoBehaviour
{
    [Header("Only works if all story zones are reactivateable")]
    List<StoryTriggerZone> zones;

    private void Awake() {
        zones = new();
        GetComponentsInChildren<StoryTriggerZone>(true, zones);
        foreach (var zone in zones)
        {
            if (!zone.Reactivateable) {
                Debug.LogError(zone.name + " is not reactivateable");
            }
        }
    }

    private void ToggleZones(StoryTriggerZone triggeredZone) {
        foreach (StoryTriggerZone zone in zones) {
            if (zone == triggeredZone) {
                zone.gameObject.SetActive(false);
            } else {
                zone.gameObject.SetActive(true);
            }
        }
    }
}
