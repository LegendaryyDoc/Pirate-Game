using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenBehavior : MonoBehaviour
{
    public Button creditsButton;
    public Button exitButton;
    public Button startButton;

    void Start()
    {
        creditsButton.onClick.AddListener(delegate { loadScene("Credits"); });
        exitButton.onClick.AddListener(exitApplication);
        startButton.onClick.AddListener(delegate { loadScene("Map"); });
    }

    void exitApplication()
    {
        Application.Quit();
    }

    void loadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
