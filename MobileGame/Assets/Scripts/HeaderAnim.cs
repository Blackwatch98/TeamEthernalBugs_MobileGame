using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeaderAnim : MonoBehaviour
{
    public GameObject PanelHeader;

    public void ShowHideHeader()
    {
        if (PanelHeader != null)
        {
            Animator animator = PanelHeader.GetComponent<Animator>();
            if (animator != null)
            {
                bool isOpen = animator.GetBool("show");
                animator.SetBool("show", !isOpen);
            }
        }
    }
}
