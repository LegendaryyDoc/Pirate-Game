using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraBehavior : MonoBehaviour
{
    public new GameObject gameObject;

    // Android
    public Button LookLeft;
    public Button LookRight;

    private void Update()
    {
        if (Application.platform == (RuntimePlatform.WindowsPlayer | RuntimePlatform.WindowsEditor))
        {
            LookLeft.onClick.AddListener(lookLeft);
            LookRight.onClick.AddListener(lookRight);

            if (Input.GetKeyDown(KeyCode.Q))
            {
                gameObject.transform.Rotate(0.0f, -90.0f, 0.0f);
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                gameObject.transform.Rotate(0.0f, 90.0f, 0.0f);
            }
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            LookLeft.onClick.AddListener(lookLeft);
            LookRight.onClick.AddListener(lookRight);
        }
    }

    public void lookLeft() {
        gameObject.transform.Rotate(0.0f, -90.0f, 0.0f);
    }
    public void lookRight() { gameObject.transform.Rotate(0.0f, 90.0f, 0.0f); }
}
