using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MazeObstacle : MonoBehaviour
{
    //when you touch this obstacle, you die

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<PlayerController2>())
        {
            RestartLevel();

        }
        if (other.gameObject.GetComponent<PlayerController_Levels>())
        {
            RestartLevel();
        }

    }

    void RestartLevel()
    {
        SceneManager.LoadSceneAsync("Lose");

    }
}
