using UnityEngine;

public class CameraMovement_Level1 : MonoBehaviour
{
    private TutorialManager tutorialManager;
    private SophomoreManager sophomoreManager;

    private bool isDialogueActive = false;
    private bool pauseCamera = true; // Variable to control camera freezing

    void Start()
    {


    }

    void Update()
    {
        // Stop camera movement if dialogue is active
        if (pauseCamera)
        {
            transform.Translate(Vector3.right * 0 * Time.deltaTime);

        }
        if (isDialogueActive)
        {
            transform.Translate(Vector3.right * 0 * Time.deltaTime);

        }

        if (!pauseCamera && !isDialogueActive)
        {
            transform.Translate(Vector3.right * 2 * Time.deltaTime);
        }
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