using UnityEngine;

public class StopTrigger : MonoBehaviour
{
    public TutorialManager tutorialManager; // Reference to the TutorialManager script

    private void Start()
    {
        // Find and link the TutorialManager if it's not already set in the inspector
        if (tutorialManager == null)
        {
            tutorialManager = FindObjectOfType<TutorialManager>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player enters the stop area
        if (other.CompareTag("Player"))
        {
            // Pause camera movement and advance the tutorial
            tutorialManager.cameraMovement.PauseCameraMovement();
            tutorialManager.AdvancePopUpIndex();  // Method in TutorialManager to increase popUpIndex
        }
    }
}
