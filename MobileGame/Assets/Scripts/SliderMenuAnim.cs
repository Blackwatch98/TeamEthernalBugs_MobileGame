using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderMenuAnim : MonoBehaviour
{
    public GameObject PanelMenu;
    public Animator animator;

    public void ShowHideMenu(string buildingName)
    {
        if (PanelMenu != null)
        {
            animator = PanelMenu.GetComponent<Animator>();
            //Text text = GameObject.FindGameObjectWithTag("Text").GetComponent<Text>();
            //text.text = buildingName;
            if (animator != null)
            {
                bool isOpen = animator.GetBool("show");
                animator.SetBool("show", !isOpen);
            }
        }
    }
    public bool getState()
    {

            animator = PanelMenu.GetComponent<Animator>();
            return animator.GetBool("show");
        
    }
}
