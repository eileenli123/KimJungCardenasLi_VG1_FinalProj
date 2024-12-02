using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class TakeTestChair : MonoBehaviour
{
    private bool hasActivatedOnce = false;  // To track if the effect has already been applied
    private bool hasBeenSkipped = false; // To track if the chair was skipped
    public float freezeDuration = 3f;  
    public TextMeshPro counter;
    public ProgressBarsControl progressBarsControl; 
    public Camera mainCamera;

    public string grade;

    //manually input GPA gem cost for grades to make it different for each level
    public float AGemCost;
    public float BGemCost;
    public float CGemCost;
    public float DGemCost;


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
            hasBeenSkipped = true;  // Mark as skipped 
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
        float currGPAGem = progressBarsControl.getCurrGPAGemCount();
        Debug.Log("curr GPA gem" + currGPAGem); 

        //A = 4, B = 3, C = 2, D = 1, F = 0 
        if (currGPAGem >= AGemCost)
        {
            progressBarsControl.enterGrade(4);
            progressBarsControl.decreaseGPA(AGemCost);
            grade = "A";
        }
        else if (currGPAGem >= BGemCost)
        {
            progressBarsControl.enterGrade(3);
            progressBarsControl.decreaseGPA(BGemCost);
            grade = "B";
        }
        else if (currGPAGem >= CGemCost)
        {
            progressBarsControl.enterGrade(2);
            progressBarsControl.decreaseGPA(CGemCost);
            grade = "C";
        }
        else if (currGPAGem >= DGemCost)
        {
            progressBarsControl.enterGrade(1);
            progressBarsControl.decreaseGPA(DGemCost);
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
        counter.text = "Grade Recieved: F";  
    }

    private IEnumerator FreezePlayerMovement(GameObject player)
    {
        PlayerController2 playerMovement = player.GetComponent<PlayerController2>();

        if (playerMovement != null)
        {
            hasActivatedOnce = true;  
            playerMovement.enabled = false;  // Disable player movement

            // Show the text and start the countdown
            counter.GetComponent<MeshRenderer>().enabled = true;
            StartCoroutine(Countdown(freezeDuration));

            yield return new WaitForSeconds(freezeDuration);  // Wait for the freeze duration (3 secs) 

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
            remainingTime -= 1f; 
        }

        counter.text = "Grade Recieved: " + grade;  // Display Grade
    }
}


