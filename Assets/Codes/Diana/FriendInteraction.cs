using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Diana
{
public class FriendInteraction : MonoBehaviour
{
    public CameraFollowPlayer cameraFollowPlayer; // Camera control for dialogue
    public GameObject dialogueUI; // Reference to dialogue UI
    public TextMeshProUGUI dialogueText; // Reference to dialogue text
    public TextMeshProUGUI nameText; // Reference to name text
    public TextMeshProUGUI[] choiceTexts; // Array of choices
    public DialogueManagement_copy dialogueManager; // Reference to dialogue manager
    public Dialogue_copy dialogueData; // Dialogue data

    private PlayerController2 playerMovement; // Reference to player movement
    private CameraMovement cameraMovement; // Reference to camera movement
    private bool isDialogueActive = false; // Ensure dialogue triggers only once

        void Start()
        {
            // Hide the dialogue UI when the game starts
            dialogueUI.SetActive(false);

            // Find player movement and camera movement scripts
            playerMovement = FindObjectOfType<PlayerController2>();
            cameraMovement = FindObjectOfType<CameraMovement>();

            if (dialogueData == null)
            {
                Debug.LogError("Dialogue_copy component missing on the DialogueData GameObject!");
            }
        }

    // Check when player enters the trigger area of the friend sprite
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isDialogueActive && dialogueData != null)  // Ensure dialogue starts only once
        {
            isDialogueActive = true; // Set the dialogue as active

            // Show the dialogue UI
            dialogueUI.SetActive(true);

            // Start the dialogue
            dialogueManager.StartDialogue(dialogueData); 

            // Shift the camera upward for the dialogue
            cameraFollowPlayer.StartDialogue(); 

            // Pause player movement and camera
            playerMovement.enabled = false; 
            if (cameraMovement != null)
            {
                cameraMovement.StartDialogue(); // Stop CameraMovement during dialogue
            }
        }
    }

    // Check when player leaves the trigger area of the friend sprite
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isDialogueActive)
        {
            isDialogueActive = false; // Set the dialogue as inactive

            // Hide the dialogue UI
            dialogueUI.SetActive(false);

            // Reset player movement and camera
            playerMovement.enabled = true;
            if (cameraMovement != null)
            {
                cameraMovement.EndDialogue();  // Resume CameraMovement after dialogue
            }

            // Reset the camera to normal follow behavior
            cameraFollowPlayer.EndDialogue();
        }
    }
}
}