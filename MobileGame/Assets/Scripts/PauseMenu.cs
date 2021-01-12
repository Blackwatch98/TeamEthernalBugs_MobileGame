using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool isGamePaused = false;


    private void resetPrefs()
    {
        //PlayerPrefs.DeleteAll();
       // Debug.Log("Zresetowałem prefsy");
        Pause();
    }

    public void Pause()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void Resume()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
