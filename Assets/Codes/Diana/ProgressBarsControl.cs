using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarsControl : MonoBehaviour
{
    public static ProgressBarsControl instance; //create an instance to be use methods in mainMenu class

    //Reference to texts to update them as progress bar updates
    public Text coinCountText;
    public Text GPAScoreText;
    public Text GPAGemText;
    public Text socialGemText;
    public Text majorText;


    //local trackers (need to still update player prefs as progress bar updates)
    public Slider GPABar;
    public Slider SocialBar;
    private int coinCount;
    private float GPAScore;
    private float numGrades;
    private string major;
    private float gpaReq;


    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        //store current level to come back to if restart level 
        PlayerPrefs.SetString("levelName", SceneManager.GetActiveScene().name);

        //local trackers (for this level) 
        GPABar.value = PlayerPrefs.GetFloat("GPA", 0f);
        SocialBar.value = PlayerPrefs.GetFloat("Social", 0f);
        coinCount = PlayerPrefs.GetInt("CoinCount", 0);
        GPAScore = PlayerPrefs.GetFloat("GPAScore", 4.0f);
        numGrades = PlayerPrefs.GetFloat("numGrades", 0f);
        major = PlayerPrefs.GetString("major", "undecided");
        gpaReq = PlayerPrefs.GetFloat("gpaReq", 2f);

        //update all the text 
        UpdateCoinCountText();
        UpdateGPAGemCountText();
        UpdateSocialGemCountText();
        UpdateGPAScoreText();
        UpdateMajorText();

        // Set max values for sliders
        GPABar.maxValue = 50f;
        SocialBar.maxValue = 50f;

        Debug.Log(numGrades);

    }


    //COIN Controls 
    public void IncreaseCoins(int amount)
    {
        coinCount += amount;
        UpdateCoinCountText();
    }


    public bool DecreaseCoins(int amount)
    {
        if (coinCount >= amount)
        {
            coinCount -= amount;
            UpdateCoinCountText();
            return true;
        }
        Debug.Log("Not enough coins to decrease");
        return false;
    }

    public void UpdateCoinCountText()
    {
        coinCountText.text = "" + coinCount;
    }



    //GPA Gem Controls 
    public void IncreaseGPA(float value)
    {
        GPABar.value = Mathf.Clamp(GPABar.value + value, 0f, GPABar.maxValue);
        UpdateGPAGemCountText();

        if (GPABar.value < 0f)
        {
            SceneManager.LoadScene("Lose");
        }
    }


    public void UpdateGPAGemCountText()
    {
        GPAGemText.text = "GPA: " + GPABar.value;
    }


    //TODO :could just pass negative value (make sure uses of decreaseGPA is removed first) 
    public void decreaseGPA(float value)
    {
        GPABar.value = Mathf.Clamp(GPABar.value - value, 0f, GPABar.maxValue);
        GPAGemText.text = "GPA: " + GPABar.value;

        if (GPABar.value < 0f)
        {
            SceneManager.LoadScene("Lose");
        }
    }

    public float getCurrGPAGemCount()
    {
        return GPABar.value;
    }

    public float getCurrGPA()
    {
        return GPAScore; 
    }

    //Social Gem Controls 
    public void IncreaseSocial(float value)
    {
        SocialBar.value = Mathf.Clamp(SocialBar.value + value, 0f, SocialBar.maxValue);
        UpdateSocialGemCountText();

        if (SocialBar.value < 0f)
        {
            SceneManager.LoadScene("Lose");
        }
    }

    public void UpdateSocialGemCountText()
    {
        socialGemText.text = "Social Status: " + SocialBar.value;
    }

    //GPA control
    public void enterGrade(float grade)
    {   //A = 4, B=3, C=2, D=1, F=0
        GPAScore = ((GPAScore * numGrades) + grade) / (numGrades + 1);
        numGrades += 1;
        UpdateGPAScoreText();
    }

    public void UpdateGPAScoreText()
    {
        GPAScoreText.text = "GPA: " + GPAScore.ToString("F1");
        //if (numGrades < 1)
        //{
        //    GPAScoreText.text = "GPA: N/A";
        //}
        //else
        //{
        //    GPAScoreText.text = "GPA: " + GPAScore.ToString("F1");
        //}
    }

    public float getNumGrades()
    {
        return numGrades;
    }


    //major control
    public void setMajor(float gpa, string majorName)
    {
        major = majorName;
        gpaReq = gpa;
        UpdateMajorText();
    }

    public void UpdateMajorText()
    {
        majorText.text = "Major: " + major + "\n(" + gpaReq.ToString("F1") + " required)";
    }


    //only set stats if player passed the level (should restart level w initial level stats) 
    public void setAllStats()
    {
        PlayerPrefs.SetFloat("GPA", GPABar.value);
        PlayerPrefs.SetFloat("Social", SocialBar.value);
        PlayerPrefs.SetFloat("numGrades", numGrades);
        PlayerPrefs.SetFloat("GPAScore", GPAScore);
        PlayerPrefs.SetInt("CoinCount", coinCount);
        PlayerPrefs.SetFloat("gpaReq", gpaReq);
        PlayerPrefs.SetString("major", major);
        PlayerPrefs.Save();
    }



    //set everything back to 0 
    public void RestartGame()
    {
        PlayerPrefs.DeleteKey("GPA");
        PlayerPrefs.DeleteKey("Social");
        PlayerPrefs.DeleteKey("CoinCount");
        PlayerPrefs.DeleteKey("GPAScore");
        PlayerPrefs.DeleteKey("numGrades");
        PlayerPrefs.DeleteKey("gpaReq");
        PlayerPrefs.DeleteKey("levelName");
        PlayerPrefs.DeleteKey("SelectedInternship"); 
        PlayerPrefs.DeleteKey("major");
    }


}
