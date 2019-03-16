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

    private void Start()
    {
        // When building, make sure to delete this later
        LookLeft.onClick.AddListener(lookLeft);
        LookRight.onClick.AddListener(lookRight);
        if (Application.platform == RuntimePlatform.Android)
        {
            LookLeft.onClick.AddListener(lookLeft);
            LookRight.onClick.AddListener(lookRight);
        }
    }

    private void Update()
    {
        if (Application.platform == (RuntimePlatform.WindowsPlayer | RuntimePlatform.WindowsEditor))
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                gameObject.transform.Rotate(0.0f, -90.0f, 0.0f);
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                gameObject.transform.Rotate(0.0f, 90.0f, 0.0f);
            }
        }
    }

    public void lookLeft() {
        gameObject.transform.Rotate(0.0f, -90.0f, 0.0f);
    }
    public void lookRight() { gameObject.transform.Rotate(0.0f, 90.0f, 0.0f); }
}
