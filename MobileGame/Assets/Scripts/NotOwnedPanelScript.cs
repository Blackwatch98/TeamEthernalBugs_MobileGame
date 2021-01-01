using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;

public class NotOwnedPanelScript
{
    private GameObject Sidebar;
    private Text typeText;
    private Button buyButton;
    private GameObject buyButtonObject, ownedButtonObject, distilleryButtonObject, casinoButtonObject, nightClubButtonObject, drugsButtonObject;
    private Button distilleryButton, casinoButton, nightClubButton, drugsButton;
    private Text priceText;
    private Text nameText;
    private BuildingClass currentBuilding;

    public NotOwnedPanelScript(GameObject sidebar, BuildingClass building)
    {

        Sidebar = GameObject.Instantiate(sidebar, sidebar.transform.parent.gameObject.transform);

        currentBuilding = building;
        Debug.Log("Nowa instancja NotOwnedPanelscript dla obiektu " + currentBuilding.transform.name);

        buyButtonObject = Sidebar.transform.Find("BuyButton").gameObject;
        //newBuyButtonObject = UnityEngine.Object.Instantiate(oldBuyButtonObject,sidebar.transform);
        //oldBuyButtonObject.SetActive(false);
        //newBuyButtonObject.SetActive(true);
        
        buyButton=buyButtonObject.GetComponent<Button>();

        ownedButtonObject = Sidebar.transform.Find("OwnButton").gameObject;

        distilleryButtonObject = Sidebar.transform.Find("DistilleryTypeButton").gameObject;
        casinoButtonObject = Sidebar.transform.Find("CasinoTypeButton").gameObject;
        nightClubButtonObject = Sidebar.transform.Find("NightClubTypeButton").gameObject;
        drugsButtonObject = Sidebar.transform.Find("DrugHollowTypeButton").gameObject;

        distilleryButton = Sidebar.transform.Find("DistilleryTypeButton").GetComponent<Button>();
        casinoButton = Sidebar.transform.Find("CasinoTypeButton").GetComponent<Button>();
        nightClubButton = Sidebar.transform.Find("NightClubTypeButton").GetComponent<Button>();
        drugsButton = Sidebar.transform.Find("DrugHollowTypeButton").GetComponent<Button>();

        priceText = Sidebar.transform.Find("Price").GetComponent<Text>();
        nameText = Sidebar.transform.Find("NamePanel").Find("Name").GetComponent<Text>();
    }

    public void setupBar()
    {
        updateSidebar();
        buyButton.onClick.AddListener(setToOwned);
        buyButton.onClick.AddListener(updateSidebar);

        distilleryButton.onClick.AddListener(() => setBuildingType("Destylarnia"));
        casinoButton.onClick.AddListener(() => setBuildingType("Kasyno"));
        nightClubButton.onClick.AddListener(() => setBuildingType("Klub Nocny"));
        drugsButton.onClick.AddListener(() => setBuildingType("Dilerka"));

        priceText.text = currentBuilding.getPrice().ToString();
        nameText.text = currentBuilding.getName();
        Debug.Log("cena "+currentBuilding.getPrice().ToString());
        Debug.Log("setupBar() " + currentBuilding.transform.name);
    }

    public void updateSidebar()
    {
        Debug.Log("UpdateSidebar z NotOwnedPanel");
        if (currentBuilding.getIsOwned())
        {
            buyButtonObject.SetActive(false);
            ownedButtonObject.SetActive(true);
        }
        else
        {
            buyButtonObject.SetActive(true);
            ownedButtonObject.SetActive(false);
        }
    }

    public void setToOwned()
    {
        currentBuilding.setIsOwned(true);
        distilleryButtonObject.SetActive(true);
        casinoButtonObject.SetActive(true);
        nightClubButtonObject.SetActive(true);
        drugsButtonObject.SetActive(true);
        Debug.Log("Kupuję " + currentBuilding.transform.name);
    }

    public void setBuildingType(string type)
    {
        currentBuilding.setType(type);
        GameObject ClassifiedSidebar = GameObject.Find("Sidebar").transform.Find("ClassifiedSidebar").gameObject;
        ClassifiedSidebar.SetActive(true);
        OwnedPanelScript script = new OwnedPanelScript(ClassifiedSidebar, currentBuilding);
        currentBuilding.setOwnedPanel(script);
        currentBuilding.getOwnedPanel().setupBar();
    }
}
