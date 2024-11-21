using UnityEngine;

public class ProfessorInteraction : MonoBehaviour
{
    public CameraMovement_Level1 cameraMovement; // Reference to the camera movement script
    public GameObject dialogueBox; // UI element to display the professor's dialogue
    private bool isPlayerNearby = false;

    void Start()
    {

        cameraMovement = FindObjectOfType<CameraMovement_Level1>();

        cameraMovement.AllowCameraMovement();


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            cameraMovement.PauseCameraMovement();

        }
    }

    void Update()
    {
        if (isPlayerNearby && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            cameraMovement.AllowCameraMovement();

            isPlayerNearby = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            cameraMovement.AllowCameraMovement();


        }
    }
}
