using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class EnterSchool_nextScene : MonoBehaviour
{
    public ProgressBarsControl progressBarsControl;



    private void OnCollisionEnter2D(Collision2D other)
    {
        //TODO: different levels have different scripts : change to be consistent
        if (other.gameObject.GetComponent<PlayerController2>() || other.gameObject.CompareTag("Player"))
        {
            string sceneName = SceneManager.GetActiveScene().name;


            //TODO: reorganize conditions (first check gpa req met)
            if (sceneName == "Tutorial")
            {
                SceneManager.LoadScene("Choose Major");
            } else if (sceneName == "Choose Major") {
                SceneManager.LoadScene("Level1");
            }
            else if (sceneName == "Choose Major 2")
            {
                SceneManager.LoadScene("Level2");
            }
            else {
                float currentGPA = PlayerPrefs.GetFloat("GPAScore", 0f);
                float minGPAReq = PlayerPrefs.GetFloat("gpaReq", 2.0f);

                if (currentGPA < minGPAReq)
                {
                    progressBarsControl.RestartGame(); //reset all stats 
                    SceneManager.LoadScene("Lose");
                } else if (sceneName == "Level1")
                {
                    SceneManager.LoadScene("Choose Major 2"); 
                }                

            }

        }
    }
}