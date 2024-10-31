using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterSchool_nextScene : MonoBehaviour
{
    public ProgressBarsControl progressBarsControl; 

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<PlayerController2>())
        {
            string sceneName = SceneManager.GetActiveScene().name;

            
            if (sceneName == "Level1")
            {
                float currentGPA = PlayerPrefs.GetFloat("GPAScore", 0f); 

                if (currentGPA >= 2.0f)
                { //passed the level
                    SceneManager.LoadScene("Level1");
                }
                else
                {
                    progressBarsControl.RestartGame(); //reset all stats 
                    SceneManager.LoadScene("Lose");
                }
            }
            else
            {
                SceneManager.LoadScene("Level1"); //TODO: chnage this -- made default to level 1 so tutorial level loads level 1 
                //add logic for other levels when made 
            }
        }
    }
}