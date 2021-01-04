using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GoldScript : MonoBehaviour
{
    public int gold;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
      gold = PlayerPrefs.GetInt("gold", 0);
      text.text = gold.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
