using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform player;
    public float regularYOffset = 1f;
    public float dialogueYOffset = 3f;
    public float zOffset = -5f;
    private bool isDialogueActive = false;

    // Update is called once per frame
    void Update()
    {


        if (isDialogueActive)
        {
            // Move the camera upward when dialogue is active
            transform.position = player.position + new Vector3(0, dialogueYOffset, zOffset);
        }
        else
        {
            // Follow the player normally
            transform.position = player.position + new Vector3(0, regularYOffset, zOffset);
        }

    }

    // Call this when dialogue starts
    public void StartDialogue()
    {
        isDialogueActive = true;
        Debug.Log("Camera moved to dialogue position.");
    }

    // Call this when dialogue ends
    public void EndDialogue()
    {
        isDialogueActive = false;
        Debug.Log("Camera moved back to normal follow position.");
    }


}
