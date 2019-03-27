using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapBehavior : MonoBehaviour {
    public Canvas pcControlCanvas;

    // Use this for initialization
    void Start() {
        pcControlCanvas.enabled = false;
    }

    void Update()
    {
        if (Application.platform == (RuntimePlatform.WindowsPlayer | RuntimePlatform.WindowsEditor))
        {
            pcControlCanvas.enabled = true;
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            pcControlCanvas.enabled = false;
        }
    }
}
