using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource or GameOver clip is missing!");
        }
    }

    public void tutorial()
    {
        PlayerPrefs.GetString("levelName", "Tutorial");
        SceneManager.LoadSceneAsync("0.Tutorial");
    }

    public void startTutorialLevel()
    {
        SceneManager.LoadSceneAsync("0.Tutorial"); //load scene 1 from build scene setting (tutorial level) 
    }

    public void goToMainMenu()
    {
        //ProgressBarsControl.instance.RestartGame(); //reset all stats to 0
        PlayerPrefs.DeleteAll(); 
        SceneManager.LoadSceneAsync("StartMenu");
    }

    public void restartLastLevel()
    {
        string lastScene = PlayerPrefs.GetString("levelName", "Tutorial");
        SceneManager.LoadScene(lastScene);
    }
}
