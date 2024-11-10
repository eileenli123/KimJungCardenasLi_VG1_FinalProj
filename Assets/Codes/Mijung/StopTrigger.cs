using UnityEngine;

public class StopTrigger : MonoBehaviour
{
    public TutorialManager tutorialManager; // Reference to the TutorialManager script
    private bool hasTriggered = false; // Boolean to ensure the trigger only happens once

    private void Start()
    {
        if (tutorialManager == null)
        {
            tutorialManager = FindObjectOfType<TutorialManager>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            tutorialManager.cameraMovement_tutorial.PauseCameraMovement();
            tutorialManager.AdvancePopUpIndex();
            hasTriggered = true;
        }
    }
}
