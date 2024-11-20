using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void tutorial()
    {
        PlayerPrefs.GetString("levelName", "Tutorial");
        SceneManager.LoadSceneAsync("Tutorial");
    }
    public void startTutorialLevel()
    {
        SceneManager.LoadSceneAsync("Tutorial"); //load scene 1 from build scene setting (tutorial level) 
    }

    public void goToMainMenu()
    {
        ProgressBarsControl.instance.RestartGame(); //reset all stats to 0
        SceneManager.LoadSceneAsync("StartMenu");
    }

    public void restartLastLevel()
    {
        string lastScene = PlayerPrefs.GetString("levelName", "Tutorial");
        SceneManager.LoadScene(lastScene); 
    }
}
