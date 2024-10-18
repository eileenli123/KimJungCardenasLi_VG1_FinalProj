using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex;
    public bool canPlayerMove = false;

    // Update is called once per frame
    void Update()
    {
        // Ensure only the pop-up at popUpIndex is active
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                popUps[i].SetActive(true);   // Show current pop-up
            }
            else
            {
                popUps[i].SetActive(false);  // Hide other pop-ups
            }
        }

        // Check for player inputs and progress the tutorial
        if (popUpIndex == 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                popUpIndex++;
                canPlayerMove = true;  // Allow the player to move after this step
            }
        }
        else if (popUpIndex == 1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                popUpIndex++;  // Progress to the next tutorial pop-up
            }
        }
    }
}
