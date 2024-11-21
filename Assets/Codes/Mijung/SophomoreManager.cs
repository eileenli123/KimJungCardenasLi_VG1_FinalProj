using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SophomoreManager : MonoBehaviour
{
    public GameObject popUp; // Single pop-up object
    public CameraMovement_Level1 cameraMovement; // Reference to CameraMovement script
    private bool isPopUpActive = true;

    void Start()
    {
        cameraMovement = FindObjectOfType<CameraMovement_Level1>(); // Get the CameraMovement script

        if (popUp != null)
        {
            popUp.SetActive(true); // Ensure the popup starts active
        }

        cameraMovement.PauseCameraMovement(); // Pause camera movement initially
    }

    void Update()
    {
        // Check for Enter key press
        if (isPopUpActive && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            popUp.SetActive(false); // Hide the popup
            cameraMovement.AllowCameraMovement(); // Resume camera movement
            isPopUpActive = false; // Prevent further updates to the popup state
        }
    }
}
