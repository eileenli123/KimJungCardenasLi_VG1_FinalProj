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
    public GameObject dialogueUI;
    public TextMeshProUGUI dialogueText; 
    public TextMeshProUGUI nameText; 
    public TextMeshProUGUI[] choiceTexts; // Array of choices
    public DialogueManagement_copy dialogueManager; 
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

            dialogueUI.SetActive(true);

            dialogueManager.StartDialogue(dialogueData); 

            dialogueManager.OnDialogueEnd += HandleDialogueEnd;

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

    private void HandleDialogueEnd()
        {
            dialogueManager.OnDialogueEnd -= HandleDialogueEnd;

            playerMovement.enabled = true;

            cameraFollowPlayer.EndDialogue();

            dialogueUI.SetActive(false);

            // Set dialogue as inactive
            isDialogueActive = false;
        }

    // Check when player leaves the trigger area of the friend sprite
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isDialogueActive)
        {
            isDialogueActive = false; // Set the dialogue as inactive

            dialogueUI.SetActive(false);

            // Reset player movement and camera
            playerMovement.enabled = true;
            cameraFollowPlayer.EndDialogue();

            if (cameraMovement != null)
            {
                cameraMovement.EndDialogue();  // Resume CameraMovement after dialogue
            }

            dialogueManager.OnDialogueEnd -= HandleDialogueEnd;
        }
    }
}
}
