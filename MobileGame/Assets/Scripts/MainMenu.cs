using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;
public class MainMenu : MonoBehaviour
{
   public static readonly double REWARD_DELAY = 1.0;
   public GameObject Panel;
   public Text RewardInfo;
   public Image Reward;
   public Sprite Gold;
   public Sprite Dollars;
   public Sprite Worker;
   public Sprite Booster;
   public static int counter = 0;

   public void PlayGame()
   {
      SceneManager.LoadScene("TestBuildings");
   }

   public void DailyReward()
   {
      if (CheckForDailyRewardAvailable())
      {
         Panel.gameObject.SetActive(true);
         int rand = UnityEngine.Random.Range(0, 4);
         // gold
         if (rand == 0)
         {
            int gold = PlayerPrefs.GetInt("gold", 0);
            int day = PlayerPrefs.GetInt("day", 0) + 1;
            int gainedGold = 100 * day;
            PlayerPrefs.SetInt("gold", gold + gainedGold);
            PlayerPrefs.SetInt("day", day);
            RewardInfo.text = "+ " + gainedGold.ToString();
            Reward.sprite = Gold;
         }
         // workers
         else if (rand == 1)
         {
            int day = PlayerPrefs.GetInt("day", 0) + 1;
            PlayerPrefs.SetInt("day", day);
            RewardInfo.text = "+ 1";
            Reward.sprite = Worker;
         }
         // dollars
         else if (rand == 2)
         {
            int dollars = PlayerPrefs.GetInt("dollars", 0);
            int day = PlayerPrefs.GetInt("day", 0) + 1;
            int gainedDollars = 10 * day;
            PlayerPrefs.SetInt("dollars", dollars + gainedDollars);
            PlayerPrefs.SetInt("day", day);
            RewardInfo.text = "+ " + gainedDollars.ToString();
            Reward.sprite = Dollars;
         }
         // booster
         else if (rand == 3)
         {
            float booster = PlayerPrefs.GetFloat("booster", 2.0f);
            int day = PlayerPrefs.GetInt("day", 0) + 1;
            float gainedBooster = 0.05f * day;
            PlayerPrefs.SetFloat("booster", booster - gainedBooster);
            PlayerPrefs.SetInt("day", day);
            RewardInfo.text = "- " + gainedBooster.ToString() + "s";
            Reward.sprite = Booster;
         }
      }
   }

   public bool CheckForDailyRewardAvailable()
   {
      string rewardDatetime = PlayerPrefs.GetString("rewardDatetime");
      if (string.IsNullOrEmpty(rewardDatetime))
      {
         PlayerPrefs.SetString("rewardDatetime", DateTime.Now.ToString());
         return true;
      }
      else
      {
         DateTime currentDatetime = DateTime.Now;
         DateTime previousDatetime = DateTime.Parse(rewardDatetime);

         double elapsedSeconds = (currentDatetime - previousDatetime).TotalSeconds;
         if (elapsedSeconds >= REWARD_DELAY)
         {
            PlayerPrefs.SetString("rewardDatetime", currentDatetime.ToString());
            return true;
         }
         return false;
      }
   }
}
