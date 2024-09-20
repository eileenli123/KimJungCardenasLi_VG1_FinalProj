using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public Slider HealthBar;
    public Slider GPABar;
    public Slider MoneyBar;
    public Slider SocialBar;

    void Start()
    {
        HealthBar.value = 0;
        GPABar.value = 0;
        MoneyBar.value = 0;
        SocialBar.value = 0;
    }

    public void UpdateHealth(float value)
    {
        HealthBar.value = Mathf.Clamp(HealthBar.value + value, 0, HealthBar.maxValue);
    }

    public void UpdateGPA(float value)
    {
        GPABar.value = Mathf.Clamp(GPABar.value + value, 0, GPABar.maxValue);
    }

    public void UpdateMoney(float value)
    {
        MoneyBar.value = Mathf.Clamp(MoneyBar.value + value, 0, MoneyBar.maxValue);
    }

    public void UpdateSocialStatus(float value)
    {
        SocialBar.value = Mathf.Clamp(SocialBar.value + value, 0, SocialBar.maxValue);
    }
}
