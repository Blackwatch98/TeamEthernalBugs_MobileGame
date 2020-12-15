using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotOwnedPanelScript
{
    private GameObject Sidebar;
    private Button buyButton;
    private GameObject buyButtonObject, ownedButtonObject;
    private Button distilleryButton, casinoButton, nightClubButton, drugsButton;
    private Text priceText;
    private Text nameText;
    private BuildingClass currentBuilding;

    public NotOwnedPanelScript(GameObject sidebar, BuildingClass building)
    {
        Sidebar = sidebar;
        currentBuilding = building;
        buyButton = Sidebar.transform.Find("BuyButton").GetComponent<Button>();
        buyButtonObject = Sidebar.transform.Find("BuyButton").gameObject;
        ownedButtonObject = Sidebar.transform.Find("OwnButton").gameObject;

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
        buyButton.onClick.AddListener(() => currentBuilding.setIsOwned(true));
        buyButton.onClick.AddListener(updateSidebar);
        distilleryButton.onClick.AddListener(() => setBuildingType("Destylarnia"));
        casinoButton.onClick.AddListener(() => setBuildingType("Kasyno"));
        nightClubButton.onClick.AddListener(() => setBuildingType("Klub Nocny"));
        drugsButton.onClick.AddListener(() => setBuildingType("Dilerka"));

        priceText.text = currentBuilding.getPrice().ToString();
        nameText.text = currentBuilding.transform.name;
    }

    public void updateSidebar()
    {
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

    public void setBuildingType(string type)
    {
        currentBuilding.setType(type);
        GameObject ClassifiedSidebar = GameObject.Find("Sidebar").transform.Find("ClassifiedSidebar").gameObject;
        ClassifiedSidebar.SetActive(true);
        OwnedPanelScript script = new OwnedPanelScript(ClassifiedSidebar, currentBuilding);
        script.setupBar();
    }
}
