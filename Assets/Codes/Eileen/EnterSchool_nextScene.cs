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
            }
            else
            {
                ProgressBarsControl.instance.setAllStats(); //set all stats earned from current level before loading next level 


                //TODO: conditional checking (May be better to enumerate instead)
                if (currentSceneName == "0.Tutorial")
                {
                    SceneManager.LoadScene("Checkpoint1");
                }
                else if (currentSceneName == "Checkpoint1")
                {
                    SceneManager.LoadScene("1.Freshman");
                }
                else if (currentSceneName == "1.Freshman")
                {
                    SceneManager.LoadScene("Checkpoint2");
                }
                else if (currentSceneName == "Checkpoint2")
                {
                    SceneManager.LoadScene("2.Sophomore");
                }
                else if (currentSceneName == "2.Sophomore")
                {
                    SceneManager.LoadScene("Checkpoint3");
                }
                else if (currentSceneName == "Checkpoint3")
                {
                    SceneManager.LoadScene("3.Junior");
                }
                else if (currentSceneName == "3.Junior")
                {
                    SceneManager.LoadScene("Checkpoint4");
                }
                else if (currentSceneName == "Checkpoint4")
                {
                    SceneManager.LoadScene("4.Senior");
                }
                else if (currentSceneName == "4.Senior")
                {
                    SceneManager.LoadScene("WinScreen");
                }


            }

        }
    }
}