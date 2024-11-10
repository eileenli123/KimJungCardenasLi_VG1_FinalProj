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


        if (other.gameObject.GetComponent<PlayerController2>() || other.gameObject.CompareTag("Player"))
        {
            string sceneName = SceneManager.GetActiveScene().name;
            Debug.Log(sceneName); 
            if(sceneName == "Tutorial")
            {
                SceneManager.LoadScene("Level1");
            } else {
                float currentGPA = PlayerPrefs.GetFloat("GPAScore", 0f);
                float minGPAReq = PlayerPrefs.GetFloat("gpaReq", 2.0f);

                if (currentGPA< minGPAReq)
                {
                    progressBarsControl.RestartGame(); //reset all stats 
                    SceneManager.LoadScene("Lose");
                } else if (sceneName == "Level1")
                {
                    SceneManager.LoadScene("Choose Major"); //TODO: replace with level 2
                } else if (sceneName == "Choose Major")
                {
                    SceneManager.LoadScene("Choose Major"); //reload curr level temporarily 
                }


            }

        }
    }
}