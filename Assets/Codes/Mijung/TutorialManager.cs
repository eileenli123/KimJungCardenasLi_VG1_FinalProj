using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex;
    public bool canPlayerMove = true;
    public CameraMovement cameraMovement; // Reference to CameraMovement script

    private bool playerNearGem = false;

    void Start()
    {
        cameraMovement = FindObjectOfType<CameraMovement>(); // Get the CameraMovement script
    }

    void Update()

    {
        cameraMovement.PauseCameraMovement();
        // Ensure only the pop-up at popUpIndex is active
        for (int i = 0; i < popUps.Length; i++)
        {
            popUps[i].SetActive(i == popUpIndex);
        }

        // Check for player inputs and progress the tutorial
        if (popUpIndex == 0)
        {
            cameraMovement.PauseCameraMovement(); // Pause the camera


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
            cameraMovement.PauseCameraMovement();

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                cameraMovement.AllowCameraMovement();
                StartCoroutine(WaitAndAdvance(5f));
                popUpIndex++;

            }
        }
        else if (popUpIndex == 3)
        {
            cameraMovement.PauseCameraMovement();

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                cameraMovement.PauseCameraMovement();
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

            if (Input.GetKeyDown(KeyCode.Space))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 8)
        {

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                cameraMovement.AllowCameraMovement();
            }
        }
    }
    private IEnumerator WaitAndAdvance(float waitTime)
    {
        float time = 0;
        while (time < waitTime)
        {
            time += Time.deltaTime;
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerNearGem = true;  // Player is now near the gem
        }
    }
}
