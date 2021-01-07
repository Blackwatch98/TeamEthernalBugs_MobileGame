using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;
public class MainManu : MonoBehaviour
{
   public static readonly double REWARD_DELAY = 20.0;
   public void PlayGame()
   {
      SceneManager.LoadScene("TestBuildings");
   }

   public void DailyReward()
   {
      if (CheckForDailyRewardAvailable())
      {
         int gold = PlayerPrefs.GetInt("gold", 0);
         int day = PlayerPrefs.GetInt("day", 0) + 1;
         int gainedGold = 100 * day;
         PlayerPrefs.SetInt("gold", gold + gainedGold);
         PlayerPrefs.SetInt("day", day);
         EditorUtility.DisplayDialog("Daily reward", "Day: " + day.ToString() + ", gained " + gainedGold.ToString() + " gold", "OK");
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
