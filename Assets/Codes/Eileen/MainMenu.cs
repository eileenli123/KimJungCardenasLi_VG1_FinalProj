using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour
{
    public void startTutorialLevel()
    {
        SceneManager.LoadSceneAsync(3); //load scene 1 from build scene setting (tutorial level) 
    }

    public void goToMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
