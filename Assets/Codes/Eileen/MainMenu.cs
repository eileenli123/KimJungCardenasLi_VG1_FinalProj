using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void tutorial()
    {
        SceneManager.LoadSceneAsync("Tutorial");
    }
    public void startTutorialLevel()
    {
        SceneManager.LoadSceneAsync("Tutorial"); //load scene 1 from build scene setting (tutorial level) 
    }

    public void goToMainMenu()
    {
        SceneManager.LoadSceneAsync("StartMenu");
    }
}
