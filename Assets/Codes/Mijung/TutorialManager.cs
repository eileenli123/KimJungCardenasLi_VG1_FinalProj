using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex;
    public bool canPlayerMove = true;
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
            cameraMovement.PauseCameraMovement(); // Pause the camera

            canPlayerMove = true;

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 1)
        {
            cameraMovement.PauseCameraMovement();

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 2)
        {

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                cameraMovement.AllowCameraMovement();
                StartCoroutine(WaitAndAdvance(5f));

            }
        }
        else if (popUpIndex == 3)
        {
            cameraMovement.PauseCameraMovement();
            canPlayerMove = true;

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 4)
        {
            cameraMovement.PauseCameraMovement();

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 5)
        {
            cameraMovement.PauseCameraMovement();

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 6)
        {
            cameraMovement.PauseCameraMovement();

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 7)
        {
            canPlayerMove = true;

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 8)
        {
            canPlayerMove = true;

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                cameraMovement.AllowCameraMovement();
            }
        }
    }
    private IEnumerator WaitAndAdvance(float waitTime)
    {
        canPlayerMove = true; // Allow player movement
        yield return new WaitForSeconds(waitTime); // Wait for the specified time
        popUpIndex++; // Advance to the next pop-up
    }

}
