using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GenerateScore : MonoBehaviour
{
    // oulets
    public TextMeshProUGUI GPA_result;
    public TextMeshProUGUI Social_result;
    public TextMeshProUGUI Major_result;
    public TextMeshProUGUI Intern_result;

    public TextMeshProUGUI GPA_score_text;
    public TextMeshProUGUI Social_score_text;
    public TextMeshProUGUI Major_score_text;
    public TextMeshProUGUI Intern_score_text;
    public TextMeshProUGUI Total_score_text;


    private void Start()
    {
        //get player stats 
        float GPA = PlayerPrefs.GetFloat("GPAScore", 4.0f);
        float socialGems = PlayerPrefs.GetFloat("Social", 0.0f);
        string major = PlayerPrefs.GetString("major", "Computer Science");
        string intern = PlayerPrefs.GetString("SelectedInternship", "Google");


        //Display player's stats result
        GPA_result.text = GPA.ToString("F1");
        Social_result.text = socialGems.ToString("F0");
        Major_result.text = major;
        Intern_result.text = intern;

        //calculate score
        double gpaScore = (GPA / 4.0) * 10000; //gpa score: (4.0 = 10k) proportion of gpa
        double socialScore = (socialGems * 200); //social score : 200 pts per gem
        double majorScore = 1000; //major score : higher gpa req -> higher score
        if (major == "Pre-Med")
        {
            majorScore = 10000; 
        } else if (major == "Computer Science")
        {
            majorScore = 7500;
        } else if (major == "Business")
        {
            majorScore = 5000;
        } else if (major == "Communications") {
            majorScore = 2500;
        }

        double internScore = 0; //inter score : higher gem cost -> higher score
        if (intern == "Google")
        {
            internScore = 10000; 
        }
        else if (intern == "Government Agency")
        {
            internScore = 7500;
        }
        else if (intern == "Local Business")
        {
            internScore = 5000;
        }
        else if (intern == "Dying Company")
        {
            internScore = 2500;
        }

        double totalScore = gpaScore + socialScore + majorScore + internScore; 


        //display score 
        GPA_score_text.text = gpaScore.ToString("F0"); 
        Social_score_text.text = socialScore.ToString("F0");
        Major_score_text.text = majorScore.ToString("F0");
        Intern_score_text.text = internScore.ToString("F0");
        Total_score_text.text = totalScore.ToString("F0");

    }

}
