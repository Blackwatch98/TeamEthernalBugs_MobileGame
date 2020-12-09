using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
public class MainManu : MonoBehaviour
{
   public void PlayGame()
   {
      SceneManager.LoadScene("TestBuildings");
   }

   public void DailyReward()
   {
      int gold = PlayerPrefs.GetInt("gold", 0);
      PlayerPrefs.SetInt("gold", gold + 100);
      EditorUtility.DisplayDialog("Daily reward", "Gained 100 gold", "OK");
   }
}
