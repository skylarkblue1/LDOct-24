using System.Collections;
using System.Collections.Generic;
using UnityEngine;


///
///  Frame limiter script by Sky~
///


public class FrameRateLimit : MonoBehaviour
{
    public int targetFrameRate = 60;

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;
    }
}
