using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectInternship : MonoBehaviour
{
    // Buttons for internships
    public Button googleButton;
    public Button govAgencyButton;
    public Button localBusinessButton;
    public Button cashierButton;

    // Reference to ProgressBarsControl for gem management
    private ProgressBarsControl progressBars;

    void Start()
    {
        // Find ProgressBarsControl in the scene
        progressBars = FindObjectOfType<ProgressBarsControl>();

        // Set up button listeners
        googleButton.onClick.AddListener(() => TrySelectInternship("Google", 10f, 10f));
        govAgencyButton.onClick.AddListener(() => TrySelectInternship("Government Agency", 5f, 6f));
        localBusinessButton.onClick.AddListener(() => TrySelectInternship("Local Business", 3f, 5f));
        cashierButton.onClick.AddListener(() => TrySelectInternship("Cashier", 1f, 1f));
    }

    void TrySelectInternship(string internshipName, float requiredGPA, float requiredSocial)
    {
        // Check if player has enough GPA and Social Gems
        if (progressBars.GPABar.value >= requiredGPA && progressBars.SocialBar.value >= requiredSocial)
        {
            // Deduct gems for successful selection
            progressBars.IncreaseGPA(-requiredGPA);
            progressBars.IncreaseSocial(-requiredSocial);

            // Additional logic for the internship
            Debug.Log($"Player selected {internshipName} internship.");
        }
        else
        {
            Debug.Log($"Not enough gems for {internshipName} internship. Required: {requiredGPA} GPA, {requiredSocial} Social Gems.");
        }
    }
}
