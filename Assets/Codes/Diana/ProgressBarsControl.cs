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
        HealthBar.value = 0f;
        GPABar.value = 0f;
        MoneyBar.value = 0f;
        SocialBar.value = 0f;

        HealthBar.maxValue = 50f;
        GPABar.maxValue = 50f;
        MoneyBar.maxValue = 50f;
        SocialBar.maxValue = 50f;
    }

     public void IncreaseHealth(float value)
    {
        HealthBar.value = Mathf.Clamp(HealthBar.value + value, 0f, HealthBar.maxValue);
        if (HealthBar.value < 0f)
        {
            RestartGame();
        }
    }

    public void IncreaseGPA(float value)
    {
        GPABar.value = Mathf.Clamp(GPABar.value + value, 0f, GPABar.maxValue);
        if (GPABar.value < 0f)
        {
            RestartGame();
        }
    }

    public void IncreaseMoney(float value)
    {
        MoneyBar.value = Mathf.Clamp(MoneyBar.value + value, 0f, MoneyBar.maxValue);
        if (MoneyBar.value < 0f)
        {
            RestartGame();
        }
    }

    public void IncreaseSocial(float value)
    {
        SocialBar.value = Mathf.Clamp(SocialBar.value + value, 0f, SocialBar.maxValue);
        if (SocialBar.value < 0f)
        {
            Debug.Log("Social is 0");
            RestartGame();
        }
    }
    private void RestartGame()
    {
        Debug.Log("Restarting game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

