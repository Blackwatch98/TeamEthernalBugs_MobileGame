using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderMenuAnim : MonoBehaviour
{
    public GameObject PanelMenu;

    public void ShowHideMenu()
    {
        if (PanelMenu != null)
        {
            Animator animator = PanelMenu.GetComponent<Animator>();
            if(animator != null)
            {
                bool isOpen = animator.GetBool("show");
                animator.SetBool("show", !isOpen);
            }
        }
    }
}
