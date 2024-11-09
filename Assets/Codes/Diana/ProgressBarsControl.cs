using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarsControl : MonoBehaviour
{
    public Slider GPABar;
    public Slider SocialBar;

    public Text coinCountText;  // Reference to the Text component for coin count display
    public Text GPAScoreText; 

    public Text GPAGemText;
    public Text socialGemText;

    private float playerMoney;
    private int coinCount;  // Variable to track the coin count
    private float GPAScore;
    private float numGrades; 


    void Start()
    {
        GPABar.value = PlayerPrefs.GetFloat("GPA", 0f);
        SocialBar.value = PlayerPrefs.GetFloat("Social", 0f);
        playerMoney = PlayerPrefs.GetFloat("PlayerMoney", 0f);
        coinCount = PlayerPrefs.GetInt("CoinCount", 0);
        GPAScore = PlayerPrefs.GetFloat("GPAScore", 4.0f);
        numGrades = PlayerPrefs.GetFloat("numGrades", 1f); 


        UpdateCoinCountText();
        GPAGemText.text = "GPA: " + GPABar.value;
        socialGemText.text = "Social Status: " + SocialBar.value;
        GPAScoreText.text = "GPA: " + GPAScore.ToString("F1");
        Debug.Log(GPAScore); 

        // Set max values for sliders
        GPABar.maxValue = 50f;
        SocialBar.maxValue = 50f;

    }

    public void IncreaseCoins(int amount)
    {
        coinCount += amount;
        PlayerPrefs.SetInt("CoinCount", coinCount);
        PlayerPrefs.Save();

        Debug.Log("Coins increased, current count: " + coinCount);
        UpdateCoinCountText();
    }

    public bool DecreaseCoins(int amount)
    {
        if (coinCount >= amount)
        {
            coinCount -= amount;
            PlayerPrefs.SetInt("CoinCount", coinCount);
            PlayerPrefs.Save();

            Debug.Log("Coins decreased, current count: " + coinCount);
            UpdateCoinCountText();
            return true;
        }
        Debug.Log("Not enough coins to decrease");
        return false;
    }

    // Update the coin count text display
    public void UpdateCoinCountText()
    {
        coinCountText.text = "" + coinCount;  // Display the coin count
    }

    public float GetMoney()
    {
        return playerMoney;
    }


    public void IncreaseGPA(float value)
    {
        GPABar.value = Mathf.Clamp(GPABar.value + value, 0f, GPABar.maxValue);
        PlayerPrefs.SetFloat("GPA", GPABar.value);
        PlayerPrefs.Save();
        GPAGemText.text = "GPA: " + GPABar.value; 

        if (GPABar.value < 0f)
        {
            RestartGame();
        }
    }


    public void decreaseGPA(float value)
    {
        GPABar.value = Mathf.Clamp(GPABar.value - value, 0f, GPABar.maxValue);
        PlayerPrefs.SetFloat("GPA", GPABar.value);
        PlayerPrefs.Save();
        GPAGemText.text = "GPA: " + GPABar.value;

        if (GPABar.value < 0f)
        {
            RestartGame();
        }
    }

    public void enterGrade(float grade)
    {   //A = 4, B=3, C=2, D=1
        GPAScore = ((GPAScore * numGrades) + grade) / (numGrades + 1); 
        PlayerPrefs.SetFloat("numGrades", numGrades + 1);
        numGrades += 1;
        PlayerPrefs.SetFloat("GPAScore", GPAScore);
        PlayerPrefs.Save();
        GPAScoreText.text = "GPA: " + GPAScore.ToString("F1");

    }


    public void IncreaseSocial(float value)
    {
        SocialBar.value = Mathf.Clamp(SocialBar.value + value, 0f, SocialBar.maxValue);
        PlayerPrefs.SetFloat("Social", SocialBar.value);  // Save progress
        PlayerPrefs.Save();
        socialGemText.text = "Social Status: " + SocialBar.value; 

        if (SocialBar.value < 0f)
        {
            Debug.Log("Social is 0");
            RestartGame();
        }
    }

    public void RestartGame()
    {
        Debug.Log("Restarting game");

      
        PlayerPrefs.DeleteKey("GPA");
        PlayerPrefs.DeleteKey("Social");
        PlayerPrefs.DeleteKey("CoinCount"); 
        PlayerPrefs.DeleteKey("GPAScore");
        PlayerPrefs.DeleteKey("numGrades");


        // Reload the current scene, which will reset all bars to 0
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name); // handle which level load depending on reason why they lost
    }


}
