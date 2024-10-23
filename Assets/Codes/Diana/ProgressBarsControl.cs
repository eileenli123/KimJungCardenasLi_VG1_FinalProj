using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarsControl : MonoBehaviour
{
    public Slider HealthBar;
    public Slider GPABar;
    public Slider MoneyBar;
    public Slider SocialBar;

    private float playerMoney;

    void Start()
    {

        HealthBar.value = PlayerPrefs.GetFloat("Health", 0f);
        GPABar.value = PlayerPrefs.GetFloat("GPA", 0f);
        MoneyBar.value = PlayerPrefs.GetFloat("Money", 0f);
        SocialBar.value = PlayerPrefs.GetFloat("Social", 0f);
        Debug.Log("Loaded Health: " + HealthBar.value);
        Debug.Log("Loaded GPA: " + GPABar.value);
        Debug.Log("Loaded Money: " + MoneyBar.value);
        Debug.Log("Loaded Social: " + SocialBar.value);

        playerMoney = PlayerPrefs.GetFloat("PlayerMoney", 0f); // Load money as float
        Debug.Log("Loaded Player Money: " + playerMoney);

        HealthBar.maxValue = 50f;
        GPABar.maxValue = 50f;
        MoneyBar.maxValue = 50f;
        SocialBar.maxValue = 50f;
    }

public bool DecreaseMoney(float amount)  
    {
        if (playerMoney >= amount)
        {
            playerMoney -= amount; // Update player money
            PlayerPrefs.SetFloat("PlayerMoney", playerMoney);  // Save the updated player money
            PlayerPrefs.Save();
            Debug.Log("Money decreased, current amount: " + playerMoney);

            // Update the MoneyBar slider
            MoneyBar.value = Mathf.Clamp(MoneyBar.value - amount, 0f, MoneyBar.maxValue);
            PlayerPrefs.SetFloat("Money", MoneyBar.value);  // Save MoneyBar progress
            PlayerPrefs.Save();

            return true; // Transaction successful
        }
        else
        {
            Debug.Log("Not enough money!");
            return false; // Not enough money
        }
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
        PlayerPrefs.SetFloat("GPA", GPABar.value);  // Save progress
        PlayerPrefs.Save();

        if (GPABar.value < 0f)
        {
            RestartGame();
        }
    }

    public void IncreaseMoney(float amount)
    {
        playerMoney += amount; // Update player money
        PlayerPrefs.SetFloat("PlayerMoney", playerMoney);  // Save the player money
        PlayerPrefs.Save();
        Debug.Log("Money increased, current amount: " + playerMoney);

        // Update the MoneyBar slider
        MoneyBar.value = Mathf.Clamp(MoneyBar.value + amount, 0f, MoneyBar.maxValue);
        PlayerPrefs.SetFloat("Money", MoneyBar.value); 
        PlayerPrefs.Save();


        if (MoneyBar.value < 0f)
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

