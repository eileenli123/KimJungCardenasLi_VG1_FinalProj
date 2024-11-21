using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeniorManager : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex;

    public CameraMovement cameraMovement; // Reference to CameraMovement script
    private bool isPopUpActive = true;


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

        switch (popUpIndex)
        {
            case 0:
            case 1:
                cameraMovement.PauseCameraMovement();
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    popUpIndex++;
                }
                break;

            case 2:
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    cameraMovement.AllowCameraMovement();
                    isPopUpActive = false;
                    popUpIndex++; // Move to next pop-up after the user presses enter
                }
                break;

            case 3:
                // Ensure the pop-up disappears when index reaches 3
                popUps[popUpIndex].SetActive(false);
                break;

            default:
                break;
        }
    }
}
