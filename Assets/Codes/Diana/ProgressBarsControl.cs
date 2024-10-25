using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarsControl : MonoBehaviour
{
    public Slider HealthBar;
    public Slider GPABar;
    public Slider SocialBar;

    public int coinCountText;  // Reference to the Text component for coin count display

    private float playerMoney;
    private int coinCount;  // Variable to track the coin count


    void Start()
    {

        HealthBar.value = PlayerPrefs.GetFloat("Health", 0f);
        GPABar.value = PlayerPrefs.GetFloat("GPA", 0f);
        SocialBar.value = PlayerPrefs.GetFloat("Social", 0f);
        playerMoney = PlayerPrefs.GetFloat("PlayerMoney", 0f);
        coinCount = PlayerPrefs.GetInt("CoinCount", 0);

        // Debug logs
        Debug.Log("Loaded Health: " + HealthBar.value);
        Debug.Log("Loaded GPA: " + GPABar.value);
        Debug.Log("Loaded Player Money: " + playerMoney);
        Debug.Log("Loaded Coin Count: " + coinCount);

        UpdateCoinCountText();

        // Set max values for sliders
        HealthBar.maxValue = 50f;
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
        coinCountText = coinCount;  // Display the coin count
    }
    public float GetMoney()
    {
        return playerMoney;
    }

    public void IncreaseHealth(float value)
    {
        HealthBar.value = Mathf.Clamp(HealthBar.value + value, 0f, HealthBar.maxValue);
        PlayerPrefs.SetFloat("Health", HealthBar.value);  // Save progress
        PlayerPrefs.Save();

        if (HealthBar.value < 0f)
        {
            RestartGame();
        }
    }

    public void IncreaseGPA(float value)
    {
        GPABar.value = Mathf.Clamp(GPABar.value + value, 0f, GPABar.maxValue);
        PlayerPrefs.SetFloat("GPA", GPABar.value);
        PlayerPrefs.Save();

        if (GPABar.value < 0f)
        {
            RestartGame();
        }
    }



    public void IncreaseSocial(float value)
    {
        SocialBar.value = Mathf.Clamp(SocialBar.value + value, 0f, SocialBar.maxValue);
        PlayerPrefs.SetFloat("Social", SocialBar.value);  // Save progress
        PlayerPrefs.Save();

        if (SocialBar.value < 0f)
        {
            Debug.Log("Social is 0");
            RestartGame();
        }
    }
    private void RestartGame()
    {
        Debug.Log("Restarting game");

        // Clear all saved progress
        PlayerPrefs.DeleteKey("Health");
        PlayerPrefs.DeleteKey("GPA");
        PlayerPrefs.DeleteKey("Money");
        PlayerPrefs.DeleteKey("PlayerMoney");
        PlayerPrefs.DeleteKey("Social");

        // Reload the current scene, which will reset all bars to 0
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

