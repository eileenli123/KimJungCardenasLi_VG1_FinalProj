using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreController : MonoBehaviour
{
    public static StoreController instance;  
    public GameObject storePanel;
    private int coinCount;
    public ProgressBarsControl ProgressBarController; //use progress bar methods to change variables 


    private void Start()
    { 
        coinCount = PlayerPrefs.GetInt("CoinCount", 0);
    }

    private void Awake()
    {
        instance = this;
        hide(); 
    }

    public void show()
    {
        storePanel.SetActive(true);
        gameObject.SetActive(true);
        Time.timeScale = 0; 
        PlayerController2.instance.isPaused = true; 
    }

    public void hide()
    {
        Time.timeScale = 1;
        if (PlayerController2.instance != null)
        {
            PlayerController2.instance.isPaused = false;
        }
        gameObject.SetActive(false); 
    }


    public void buyTutoring(int price)
    {
        if (ProgressBarController.DecreaseCoins(price)) //check that there is enough coins + decrease coins
        {
            ProgressBarController.IncreaseGPA(3f); //3 GPA gems
        } 
    }


    public void buyHostParty(int price)
    {
        if (ProgressBarController.DecreaseCoins(price))
        {
            ProgressBarController.IncreaseSocial(3f); //3 Social gems
        }
    }


}
