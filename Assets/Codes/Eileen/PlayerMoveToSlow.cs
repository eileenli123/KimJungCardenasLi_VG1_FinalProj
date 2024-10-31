using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMoveToSlow : MonoBehaviour
{
    public Camera cameraTransform;
    public float loseOffset = 0.1f; // Offset for the left edge, default is slightly inside the view
    public ProgressBarsControl progressBarsControl; 

    void Update()
    {
        // Calculate the left edge of the camera in world space
        float leftEdge = cameraTransform.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + loseOffset;

        // Check if the player's position is to the left of the left edge of the camera
        if (transform.position.x < leftEdge)
        {
            RestartLevel();
        }
    }

    void RestartLevel()
    {
        if (progressBarsControl != null)
        {
            progressBarsControl.RestartGame();  // Call RestartGame to reset all progress
        }
        SceneManager.LoadSceneAsync("Lose");

    }

}
