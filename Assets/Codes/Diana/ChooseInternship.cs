using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace Diana
{
    public class ChooseInternship : MonoBehaviour
    {
        [SerializeField] public TextMeshProUGUI selectedInternshipText; // Display selected internship
        [SerializeField] public TextMeshProUGUI[] choiceTexts;         // Text for each choice button
        [SerializeField] public Button[] choiceButtons;               // Buttons for each choice
        [SerializeField] public string[] internships;                 // List of internships
        [SerializeField] public float[] academicGemRequirements;      // Academic gem requirements
        [SerializeField] public float[] socialGemRequirements;        // Social gem requirements

        public ProgressBarsControl progressBarControl;

        void Start()
        {
            progressBarControl = FindObjectOfType<ProgressBarsControl>();

            // Set up the choice buttons
            for (int i = 0; i < choiceButtons.Length; i++)
            {
                int index = i;
                choiceButtons[i].onClick.AddListener(() => SelectInternship(index));
                choiceTexts[i].text = $"{internships[i]} ({academicGemRequirements[i]} academic gems, {socialGemRequirements[i]} social gems)";
            }
        }

        public void SelectInternship(int index)
        {
            if (progressBarControl.GPABar.value >= academicGemRequirements[index] &&
                progressBarControl.SocialBar.value >= socialGemRequirements[index])
            {
                progressBarControl.IncreaseGPA(-academicGemRequirements[index]);
                progressBarControl.IncreaseSocial(-socialGemRequirements[index]);
                selectedInternshipText.text = $"Intern at: {internships[index]}";

                PlayerPrefs.SetString("SelectedInternship", internships[index]);
                PlayerPrefs.Save();
                foreach (Button button in choiceButtons)
                {
                    button.interactable = false;
                }

                Debug.Log($"Selected Internship: {internships[index]}");
            }
            else
            {
                selectedInternshipText.text = "Not enough gems!";
            }
        }
    }
}
