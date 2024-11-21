using UnityEngine;

public class MajorTrigger : MonoBehaviour
{
    public PlayerController_Levels playerController;
    public ChooseMajor chooseMajor;

    private void Start()
    {
        // Ensure references are valid
        if (playerController == null || chooseMajor == null)
        {
            Debug.LogError("References for PlayerController or ChooseMajor are not set!");
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerController.PausePlayer();
            chooseMajor.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerController.ResumePlayer();
            chooseMajor.enabled = false;
        }
    }
}
