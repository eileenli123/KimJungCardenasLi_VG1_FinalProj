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

        HealthBar.maxValue = 100f;
        GPABar.maxValue = 100f;
        MoneyBar.maxValue = 100f;
        SocialBar.maxValue = 100f;
    }

     public void IncreaseHealth(float value)
    {
        HealthBar.value = Mathf.Clamp(HealthBar.value + value, 0f, HealthBar.maxValue);
    }

    public void IncreaseGPA(float value)
    {
        GPABar.value = Mathf.Clamp(GPABar.value + value, 0f, GPABar.maxValue);
    }

    public void IncreaseMoney(float value)
    {
        MoneyBar.value = Mathf.Clamp(MoneyBar.value + value, 0f, MoneyBar.maxValue);
    }

    public void IncreaseSocial(float value)
    {
        SocialBar.value = Mathf.Clamp(SocialBar.value + value, 0f, SocialBar.maxValue);
    }
}