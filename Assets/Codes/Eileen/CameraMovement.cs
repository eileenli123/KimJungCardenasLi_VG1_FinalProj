using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float cameraSpeed;
    private TutorialManager tutorialManager;
    private bool isDialogueActive = false; 
void Start()
    {
        
        tutorialManager = FindObjectOfType<TutorialManager>();
    }

    void Update()
    {
        // Stop camera movement if dialogue is active
        if (isDialogueActive)
        {
            return;
        }

        // Check if the player is allowed to move
        if (tutorialManager != null && !tutorialManager.canPlayerMove)
        {
            return;  // Exit update if the player is not allowed to move yet
        }

        // Move the camera when the player is allowed to move
        transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime);
    }

    public void StartDialogue()
    {
        isDialogueActive = true;
    }

    public void EndDialogue()
    {
        isDialogueActive = false;
    }
}
