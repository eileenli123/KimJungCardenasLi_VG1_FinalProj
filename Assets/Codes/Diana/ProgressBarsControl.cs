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

        HealthBar.maxValue = 50f;
        GPABar.maxValue = 50f;
        MoneyBar.maxValue = 50f;
        SocialBar.maxValue = 50f;
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

    public void IncreaseMoney(float value)
    {
        MoneyBar.value = Mathf.Clamp(MoneyBar.value + value, 0f, MoneyBar.maxValue);
        PlayerPrefs.SetFloat("Money", MoneyBar.value);  // Save progress
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
        PlayerPrefs.DeleteKey("Social");
        // Reload the current scene, which will reset all bars to 0
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

