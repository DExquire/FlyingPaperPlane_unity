using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdsButton : MonoBehaviour
{
    public int numberOfAdShows;
    public Button rewardedButton;

    private void Awake()
    {
        GetInfo();
     //   Debug.Log(PlayerPrefs.GetString("LastRewardedDate"));
        if (PlayerPrefs.GetString("LastRewardedDate") != System.DateTime.Now.DayOfYear.ToString())
        {
            numberOfAdShows = 0;
        }

     //   CheckAvailability();
    }

    private void Update()
    {
     //   CheckAvailability();
    }

    public void SaveInfo()
    {
        numberOfAdShows += 1;
        PlayerPrefs.SetInt("showedAds", numberOfAdShows);
        PlayerPrefs.SetString("LastRewardedDate", System.DateTime.Now.DayOfYear.ToString());
        CheckAvailability();
    }

    public void GetInfo()
    {
        numberOfAdShows = PlayerPrefs.GetInt("showedAds");
    }

    public void CheckAvailability()
    {
        if (numberOfAdShows < 3)
        {
            
            rewardedButton.interactable = true;
        }
        else
        {
         //   if (rewardedButton.interactable)
                rewardedButton.interactable = false;
        }
    }
}
