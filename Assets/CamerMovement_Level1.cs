using UnityEngine;

public class CameraMovement_Level1 : MonoBehaviour
{
    public float cameraSpeed = 2f; // Speed of the camera movement
    private TutorialManager tutorialManager;
    private bool isDialogueActive = false;
    private bool pauseCamera = true; // Variable to control camera freezing

    void Start()
    {
        tutorialManager = FindObjectOfType<TutorialManager>();
    }

    void Update()
    {
        // Stop camera movement if dialogue is active
        if (pauseCamera == true)
        {
            transform.Translate(Vector3.right * 0 * Time.deltaTime);
        }
        if (isDialogueActive)
        {
            transform.Translate(Vector3.right * 0 * Time.deltaTime);
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