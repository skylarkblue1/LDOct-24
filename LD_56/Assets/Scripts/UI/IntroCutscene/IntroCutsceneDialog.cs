using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Threading;
using Unity.VisualScripting.Antlr3.Runtime;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroCutsceneDialog : MonoBehaviour
{
    [SerializeField]
    private Canvas parentCanvas;

    [SerializeField]
    private GameObject clickToContinueObject;
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private float speechSpeedInSeconds;
    [SerializeField]
    private float waitToShowContinueInSeconds;
    private float speechDelayCount;

    private int currentLine;
    private int currentChar;
    private bool isLineDone;
    private bool queueContinue;

    private bool clickToggle;

    private CanvasGroup bgCanvas;
    private FadeUI bgFadeScript;

    private CanvasGroup continueCanvas;
    private FadeUI continueFadeScript;

    // TODO make into a lang file rather than hardcode
    private string[] dialogue =
    {
        "It had been thirty years since anyone had opened the dollhouse.",
        "It had been thirty years since anyone had played with the dolls.",
        "They had been put in the attic, their girl had grown up, and they were forgotten�",
        "Until�. the attic door swung open.",
        "�Come up here, honey! I want to show you some of my old toys.�",
        "�I wonder if Mom and Dad kept the�they did! My dollhouse!�",
        "�I used to LOVE this thing. Here, let�s bring it downstairs so you can play with it.�",
        "�See? The rooms shift around, so you can rearrange the house however you want!�",
        "�I don�t like these toys. They�re so old.�",
        "�What? No, you�ll love these! They�re not �old,� they�re just��",
        "�Classic.�",
        "�You�ll have fun. Use your imagination. Be resourceful.�",
        "�You�re a smart boy. You can figure out how to keep yourself entertained.�",
        "The dollhouse shifted. The dolls slid to the side in a stiff plastic heap.",
        "Betsy�s head poked out of the bedroom doorway. She smiled at the sky.",
        "�Hold on, I remember you� it has been a while hasn't it?�",
        "�This is Betsy. She was my favorite.",
        "The Mother combed her fingers through Betsy's hair, making sure to push it back from her face.",
        "�She was mas my best friend. Get it? Betsy my bestie.�",
        "Betsy was lifted out of the doorway. Smiling at the sky.",
        "She was so happy to see her girl again.",
        "�I�ll bring her back in a minute. Play with the other dolls in the meantime.�",
        "�Just� don�t do anything weird.�",
        "After staying untouched for thirty years, Betsy is separated from her fellow toys.",
        "She locks eyes with Polly, her closest companion, as she is carried away.",
        "�Finally, we�ll be played with again! I told everyone it would happen one day!�",
        "Polly didn�t look so sure�",
    };

    private void Start()
    {
        bgCanvas = parentCanvas.GetComponent<CanvasGroup>();
        bgFadeScript = parentCanvas.GetComponent<FadeUI>();
        bgCanvas.alpha = 0.0f;

        continueCanvas = clickToContinueObject.GetComponent<CanvasGroup>();
        continueFadeScript = clickToContinueObject.GetComponent<FadeUI>();
    }

    void Update()
    {
        if (bgFadeScript.toggleFadeOut)
        {
            text.SetText(dialogue[dialogue.Length - 1]);
            return;
        }

        float deltaTime = Time.deltaTime;
        string currentSentence = dialogue[currentLine];
        char[] sentenceCharArray = currentSentence.ToCharArray();
        char[] presentedCharArray = new char[sentenceCharArray.Length];

        // Mouse Listener
        if (isAnyMouseButtonDown() && !clickToggle)
        {
            currentLine += 1;
            currentChar = 0;
            queueContinue = false;
            isLineDone = false;

            clickToggle = true;

            clickToContinueObject.SetActive(false);

            if (currentLine >= dialogue.Length)
            {
                //Proceed to game
                bgFadeScript.toggleFadeOut = true;
                StartCoroutine(WaitForFade());
                return;
            }
        }
        else if (isAllMouseButtonUp())
        {
            clickToggle = false;
        }

        if (isLineDone) return;
        
        // Char scroller
        if (currentChar >= sentenceCharArray.Length)
        {
            queueContinue = true;
            isLineDone = true;
            Invoke(nameof(clickContinueHandler), waitToShowContinueInSeconds);
            return;
        }

        if (speechDelayCount >= speechSpeedInSeconds)
        {
            currentChar += 1;
            for(int i = 0; i < currentChar; i++)
                presentedCharArray[i] = sentenceCharArray[i];

            text.SetText(presentedCharArray);
            speechDelayCount = 0;
            return;
        }

        speechDelayCount += deltaTime;
        
    }

    void clickContinueHandler()
    {
        if (queueContinue)
        {
            continueCanvas.alpha = 0;
            continueFadeScript.toggleFadeOut = false;
            clickToContinueObject.SetActive(true);
            queueContinue = false;
        }
    }

    IEnumerator WaitForFade()
    {
        yield return new WaitUntil(isAlphaReady);
        SceneManager.LoadScene(2);
    }

    bool isAlphaReady()
    {
        return bgCanvas.alpha <= 0;
    }


    bool isAnyMouseButtonDown()
    {
        return Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2);
    }

    bool isAllMouseButtonUp()
    {
        return Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2);
    }
}
