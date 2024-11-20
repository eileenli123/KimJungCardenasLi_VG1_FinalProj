using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class EnterSchool_nextScene : MonoBehaviour
{
    private string currentSceneName;

    private void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        Debug.Log("new level started: " + currentSceneName); 
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        //TODO: different levels have different scripts -- change to be consistent
        if (other.gameObject.GetComponent<PlayerController2>() || other.gameObject.CompareTag("Player"))
        {
            float currentGPA = PlayerPrefs.GetFloat("GPAScore", 4f);
            float minGPAReq = PlayerPrefs.GetFloat("gpaReq", 2.0f);

            if (currentGPA < minGPAReq)
            {
                Debug.Log("GPA: " + currentSceneName + "did not meet req: " + minGPAReq); 
                SceneManager.LoadScene("Lose"); //just goes to lose screen (doesnt reset stats until main menu pressed) 
            } else
            {
                ProgressBarsControl.instance.setAllStats(); //set all stats earned from current level before loading next level 


                //TODO: conditional checking (May be better to enumerate instead)
                if (currentSceneName == "Tutorial")
                {
                    SceneManager.LoadScene("Choose Major");
                } else if (currentSceneName == "Choose Major")
                {
                    SceneManager.LoadScene("Level1");
                }
                else if (currentSceneName == "Level1")
                {
                    SceneManager.LoadScene("Choose Major 2");
                }
                else if (currentSceneName == "Choose Major 2")
                {
                    SceneManager.LoadScene("Level2");
                }

            }

        }
    }
}