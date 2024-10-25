using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex;
    public bool canPlayerMove = false;
    public CameraMovement cameraMovement; // Reference to CameraMovement script

    void Start()
    {
        cameraMovement = FindObjectOfType<CameraMovement>(); // Get the CameraMovement script
    }

    void Update()
    {
        // Ensure only the pop-up at popUpIndex is active
        for (int i = 0; i < popUps.Length; i++)
        {
            popUps[i].SetActive(i == popUpIndex);
        }

        // Check for player inputs and progress the tutorial
        if (popUpIndex == 0)
        {
            cameraMovement.PauseCameraMovement();

            canPlayerMove = true;  // Allow player to move

            // Start the coroutine to wait for 5 seconds and then progress
            StartCoroutine(WaitAndProgress(5f));
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 1)
        {
            StartCoroutine(WaitAndProgress(3f));
            cameraMovement.AllowCameraMovement();



        }
        else if (popUpIndex == 2)
        {
            cameraMovement.PauseCameraMovement();
            StartCoroutine(WaitAndProgress(5f));
            if (Input.GetKeyDown(KeyCode.Space))
            {
                canPlayerMove = true;  // Allow the player to move after this step
                popUpIndex++;  // Progress to the next tutorial pop-up
                cameraMovement.AllowCameraMovement(); // Indicate to the camera that it can move again

            }

        }
    }

    private IEnumerator WaitAndProgress(float waitTime)
    {

        // Wait for the specified time while allowing player movement
        yield return new WaitForSeconds(waitTime);

    }
}
