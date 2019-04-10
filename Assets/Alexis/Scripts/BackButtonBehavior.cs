using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackButtonBehavior : MonoBehaviour
{
    private Button backButton;

    void Start()
    {
        backButton = this.GetComponent<Button>();
        backButton.onClick.AddListener(back);
    }

    void back()
    {
        SceneManager.LoadScene("Title Screen");
    }
}
