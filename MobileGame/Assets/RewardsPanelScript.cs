using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardsPanelScript : MonoBehaviour
{
   public GameObject Panel;
   
    public void HidePanel()
    {
      Panel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void ShowPanel()
    {
      Panel.gameObject.SetActive(true);
    }
}
