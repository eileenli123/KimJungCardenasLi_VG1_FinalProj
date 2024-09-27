using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterSchool_nextScene : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.GetComponent<PlayerMovement>())
        {
            print(other.gameObject.name);
 

            string sceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName);
        }
    }
}
