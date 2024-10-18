using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float cameraSpeed;
    private TutorialManager tutorialManager;

    void Start()
    {
        // Find the TutorialManager in the scene
        tutorialManager = FindObjectOfType<TutorialManager>();
    }

    void Update()
    {
        // Check if the player is allowed to move
        if (tutorialManager != null && !tutorialManager.canPlayerMove)
        {
            return;  // Exit update if the player is not allowed to move yet
        }

        // Move the camera when the player is allowed to move
        transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime);
    }
}
