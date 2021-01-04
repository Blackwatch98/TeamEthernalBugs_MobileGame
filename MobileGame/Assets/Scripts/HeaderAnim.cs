using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeaderAnim : MonoBehaviour
{
    public GameObject PanelHeader;

    public void ShowHideHeader(string buildingName)
    {
        if (PanelHeader != null)
        {
            Animator animator = PanelHeader.GetComponent<Animator>();
            Text text = GameObject.Find("Name").GetComponent<Text>();
            text.text = buildingName;
            if (animator != null)
            {
                bool isOpen = animator.GetBool("show");
                animator.SetBool("show", !isOpen);
            }
        }
    }
}
