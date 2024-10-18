using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class ChairTransaction : MonoBehaviour
{
    private bool hasActivatedOnce = false;  // To track if the effect has already been applied
    public float freezeDuration = 3f;  // Time the player will be frozen
    public TextMeshPro textMeshPro;  // Reference to the TextMeshPro component in the Counter object
    public GameObject GPA_gem_prefab; 

    private void Start()
    {
        textMeshPro.GetComponent<MeshRenderer>().enabled = false; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasActivatedOnce)
        {
            Debug.Log("Collision detected with player"); 
            // Check if the collision is from the top (upward normal)
            foreach (ContactPoint2D contact in collision.contacts)
            {
                Debug.Log("Contact point normal: " + contact.normal);
                // Check if the player collided from above (normal.y should be positive and close to 1)
                if (contact.normal.y < - 0.9f)
                {
                    StartCoroutine(FreezePlayerMovement(collision.gameObject));
                    break;
                }
            }
        }
    }

    private IEnumerator FreezePlayerMovement(GameObject player)
    {
        PlayerController2 playerMovement = player.GetComponent<PlayerController2>();

        if (playerMovement != null)
        {
   
            hasActivatedOnce = true;  // Mark that the effect has occurred
            playerMovement.enabled = false;  // Disable player movement

            // Show the text and start the countdown
            textMeshPro.GetComponent<MeshRenderer>().enabled = true; 
            StartCoroutine(Countdown(freezeDuration));

            yield return new WaitForSeconds(freezeDuration);  // Wait for the freeze duration

            playerMovement.enabled = true;  // Re-enable player movement
            textMeshPro.gameObject.SetActive(false);  // Hide text after countdown
            GameObject newGPAgem = Instantiate(GPA_gem_prefab);

            // Move the gem to a much higher position (Y axis) for debugging
            Vector3 gemPosition = textMeshPro.transform.position;
            gemPosition.y += 3f;  // Move the gem 10 units above the text for debugging
            newGPAgem.transform.position = gemPosition;
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
            textMeshPro.text = remainingTime.ToString("F0");  // Display remaining time as an integer
            yield return new WaitForSeconds(1f);  // Wait for 1 second
            remainingTime -= 1f;  // Reduce time by 1 second
        }

        textMeshPro.text = "0";  // Set text to "0" when time is up
    }
}
