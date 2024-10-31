using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class TakeTestChair : MonoBehaviour
{
    private bool hasActivatedOnce = false;  // To track if the effect has already been applied
    private bool hasBeenSkipped = false; // To track if the chair was skipped
    public float freezeDuration = 3f;  // Time the player will be frozen
    public TextMeshPro counter;
    public ProgressBarsControl progressBarsControl; //ref to object that controls scores
    public Camera mainCamera;

    public string grade;

    private void Start()
    {
        counter.GetComponent<MeshRenderer>().enabled = false;
    }

    private void Update()
    {
        // Check if the chair is off-screen to the left
        float cameraLeftEdge = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;

        // If the camera's left edge passes the chair's position and it hasn't been used, assign an F
        if (!hasActivatedOnce && !hasBeenSkipped && transform.position.x < cameraLeftEdge)
        {
            AssignFGrade();
            hasBeenSkipped = true;  // Mark as skipped to prevent repeated F assignments
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasActivatedOnce)
        {
            // Check if the collision is from the top (upward normal)
            foreach (ContactPoint2D contact in collision.contacts)
            {
                Debug.Log("Contact point normal: " + contact.normal);
                // Check if the player collided from above (normal.y should be positive and close to 1)
                if (contact.normal.y < -0.9f)
                {
                    StartCoroutine(FreezePlayerMovement(collision.gameObject));
                    break;
                }
            }
        }
    }

    private void takeTest()
    {
        float currGPAGem = PlayerPrefs.GetFloat("GPA", 0f);

        if (currGPAGem >= 4)
        {
            progressBarsControl.enterGrade(4);
            progressBarsControl.decreaseGPA(4);
            grade = "A";
        }
        else if (currGPAGem == 3)
        {
            progressBarsControl.enterGrade(3);
            progressBarsControl.decreaseGPA(3);
            grade = "B";
        }
        else if (currGPAGem == 2)
        {
            progressBarsControl.enterGrade(2);
            progressBarsControl.decreaseGPA(2);
            grade = "C";
        }
        else if (currGPAGem == 1)
        {
            progressBarsControl.enterGrade(1);
            progressBarsControl.decreaseGPA(1);
            grade = "D";
        }
        else
        {
            AssignFGrade();
        }
    }

    private void AssignFGrade()
    {
        progressBarsControl.enterGrade(0);  // Assign F grade
        grade = "F";
        counter.GetComponent<MeshRenderer>().enabled = true;
        counter.text = "Grade Recieved: F";  // Display "F" grade if skipped
    }

    private IEnumerator FreezePlayerMovement(GameObject player)
    {
        PlayerController2 playerMovement = player.GetComponent<PlayerController2>();

        if (playerMovement != null)
        {
            hasActivatedOnce = true;  // Mark that the effect has occurred
            playerMovement.enabled = false;  // Disable player movement

            // Show the text and start the countdown
            counter.GetComponent<MeshRenderer>().enabled = true;
            StartCoroutine(Countdown(freezeDuration));

            yield return new WaitForSeconds(freezeDuration);  // Wait for the freeze duration

            playerMovement.enabled = true;  // Re-enable player movement
            takeTest();
        }
        else
        {
            print("could not get player movement component");
        }
    }

    private IEnumerator Countdown(float duration)
    {
        float remainingTime = duration;

        // Update the text each second
        while (remainingTime > 0)
        {
            counter.text = remainingTime.ToString("F0");  // Display remaining time as an integer
            yield return new WaitForSeconds(1f);  // Wait for 1 second
            remainingTime -= 1f;  // Reduce time by 1 second
        }

        counter.text = "Grade Recieved: " + grade;  // Set text to display grade
    }
}


