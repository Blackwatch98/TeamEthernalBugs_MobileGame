using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackMarketMenu : MonoBehaviour
{
    private GameObject blackMarketMenu, dollarsIAP, bribeMenu, boosterMenu, hireMenu;
    private Button buyDollarsBtn, boosterButton, bribeButton, hireButton;
    
    private void Start()
    {
        blackMarketMenu = gameObject;
        dollarsIAP = blackMarketMenu.transform.Find("BlackMarketDollarsSidebar").gameObject;
        boosterMenu = blackMarketMenu.transform.Find("BlackMarketMegaBoosterSidebar").gameObject;
        bribeMenu = blackMarketMenu.transform.Find("BlackMarketBribeSidebar").gameObject;
        hireMenu = blackMarketMenu.transform.Find("BlackMarketWorkerSidebar").gameObject;

        Debug.Log(dollarsIAP + " " + boosterMenu + " " + bribeMenu + " " + hireMenu);

        buyDollarsBtn = blackMarketMenu.transform.Find("BuyDollarsBtn").GetComponent<Button>();
        buyDollarsBtn.onClick.AddListener(() => dollarsIAP.SetActive(true));
        initDollarsIAP();

        boosterButton = blackMarketMenu.transform.Find("BoosterButton").GetComponent<Button>();
        boosterButton.onClick.AddListener(() => boosterMenu.SetActive(true));
        initBoosterMenu();

        bribeButton = blackMarketMenu.transform.Find("BribeButton").GetComponent<Button>();
        bribeButton.onClick.AddListener(() => bribeMenu.SetActive(true));
        initBribeMenu();

        hireButton = blackMarketMenu.transform.Find("HireButton").GetComponent<Button>();
        hireButton.onClick.AddListener(() => hireMenu.SetActive(true));
        initHireMenu();
    }

    public void initDollarsIAP()
    {
        Button cancelBtn = dollarsIAP.transform.Find("BackButton").GetComponent<Button>();
        cancelBtn.onClick.AddListener(() => dollarsIAP.SetActive(false));
        cancelBtn.onClick.AddListener(() => Debug.Log("Click"));
    }

    public void initBoosterMenu()
    {
        Button cancelBtn2 = boosterMenu.transform.Find("BackButton").GetComponent<Button>();
        cancelBtn2.onClick.AddListener(() => boosterMenu.SetActive(false));
        cancelBtn2.onClick.AddListener(() => Debug.Log("Click"));
    }

    public void initBribeMenu()
    {
        Button cancelBtn3 = bribeMenu.transform.Find("BackButton").GetComponent<Button>();
        cancelBtn3.onClick.AddListener(() => bribeMenu.SetActive(false));
    }

    public void initHireMenu()
    {
        Button cancelBtn4 = hireMenu.transform.Find("BackButton").GetComponent<Button>();
        cancelBtn4.onClick.AddListener(() => hireMenu.SetActive(false));
    }
}
