using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FadeUI : MonoBehaviour
{
    [SerializeField]
    private float fadeSpeed;

    public bool toggleFadeOut;

    private CanvasGroup canvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.gameObject.activeSelf)
        {
            float fadeFactor = fadeSpeed / 1000;
            canvasGroup.alpha = toggleFadeOut ? canvasGroup.alpha - fadeFactor : canvasGroup.alpha + fadeFactor;
            return;
        }

        canvasGroup.alpha = toggleFadeOut ? 1 : 0;
    }
}
