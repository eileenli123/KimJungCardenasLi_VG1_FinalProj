using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex;
    public CameraMovement_Tutorial cameraMovement_tutorial; // Reference to CameraMovement script


    void Start()
    {
        cameraMovement_tutorial = FindObjectOfType<CameraMovement_Tutorial>(); // Get the CameraMovement script
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
            case 3:
            case 4:
            case 5:
            case 6:
                cameraMovement_tutorial.PauseCameraMovement();
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    popUpIndex++;
                }
                break;

            case 2:
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                {
                    cameraMovement_tutorial.AllowCameraMovement();
                }

                break;

            case 7:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    popUpIndex++;
                }
                break;

            case 8:
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    cameraMovement_tutorial.AllowCameraMovement();
                }
                break;

            default:
                break;
        }
    }

    private IEnumerator WaitAndAdvance(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }



    public void AdvancePopUpIndex()
    {
        popUpIndex++;
    }
}
