using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button resetButton = gameObject.GetComponent<Button>();
        resetButton.onClick.AddListener(() => resetPrefs());
    }

    private void resetPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Zresetowałem prefsy");
    }
}
