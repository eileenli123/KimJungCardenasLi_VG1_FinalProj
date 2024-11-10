using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement_Tutorial : MonoBehaviour
{
    public float cameraSpeed; // Speed of the camera movement
    private TutorialManager tutorialManager;
    private bool isDialogueActive = false;
    private bool pauseCamera = true; // Variable to control camera freezing

    void Start()
    {
        tutorialManager = FindObjectOfType<TutorialManager>();
    }

    void Update()
    {
        if (pauseCamera)
        {
            transform.Translate(Vector3.right * 0 * Time.deltaTime);

        }

        if (!pauseCamera && !isDialogueActive)
        {
            transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime);
        }
    }


    public void StartDialogue()
    {
        isDialogueActive = true;
        PauseCameraMovement();
    }

    public void EndDialogue()
    {
        isDialogueActive = false;
    }

    // Method to freeze camera movement
    public void PauseCameraMovement()
    {
        pauseCamera = true; // Set the flag to freeze the camera
    }

    // Method to allow camera movement
    public void AllowCameraMovement()
    {
        pauseCamera = false; // Reset the flag to allow camera movement
    }
}
