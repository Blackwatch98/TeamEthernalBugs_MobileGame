using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackMarketMenu : MonoBehaviour
{
    private GameObject blackMarketMenu, blackMarketIAP;
    private Button buyDollarsBtn;
    void Start()
    {
        blackMarketMenu = gameObject;
        buyDollarsBtn = blackMarketMenu.transform.Find("BuyDollarsBtn").GetComponent<Button>();
        buyDollarsBtn.onClick.AddListener(() => blackMarketIAP.SetActive(true));
        initDollarsIAP();
    }

    
    void Update()
    {
        
    }

    public void initDollarsIAP()
    {
        blackMarketIAP = gameObject.transform.Find("BlackMarket_IAP").gameObject;
        Button cancelBtn = blackMarketIAP.transform.Find("cancelBtn").GetComponent<Button>();
        cancelBtn.onClick.AddListener(() => blackMarketIAP.SetActive(false));
    }
}
