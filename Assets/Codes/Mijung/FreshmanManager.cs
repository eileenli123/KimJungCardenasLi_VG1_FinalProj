using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreshmanManager : MonoBehaviour
{
    public GameObject popUp; // Single pop-up object
    public CameraMovement_Tutorial cameraMovement_tutorial; // Reference to CameraMovement script
    private bool isPopUpActive = true;

    void Start()
    {
        cameraMovement_tutorial = FindObjectOfType<CameraMovement_Tutorial>(); // Get the CameraMovement script

        if (popUp != null)
        {
            popUp.SetActive(true); // Ensure the popup starts active
        }

        cameraMovement_tutorial.PauseCameraMovement(); // Pause camera movement initially
    }

    void Update()
    {
        // Check for Enter key press
        if (isPopUpActive && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            popUp.SetActive(false); // Hide the popup
            cameraMovement_tutorial.AllowCameraMovement(); // Resume camera movement
            isPopUpActive = false; // Prevent further updates to the popup state
        }
    }
}
