using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotOwnedPanelScript
{
    private GameObject DefaultSidebar;
    private Button buyButton;
    private Button distilleryButton, casinoButton, nightClubButton, drugsButton;
    private Text priceText;
    private Text nameText;
    private BuildingClass currentBuilding;

    public NotOwnedPanelScript(GameObject sidebar, BuildingClass building)
    {
        DefaultSidebar = sidebar;
        currentBuilding = building;
        buyButton = DefaultSidebar.transform.Find("InnerBox").Find("BuyButton").GetComponent<Button>();
        distilleryButton = DefaultSidebar.transform.Find("InnerBox").Find("DistilleryTypeButton").GetComponent<Button>();
        casinoButton = DefaultSidebar.transform.Find("InnerBox").Find("CasinoTypeButton").GetComponent<Button>();
        nightClubButton = DefaultSidebar.transform.Find("InnerBox").Find("NightClubTypeButton").GetComponent<Button>();
        drugsButton = DefaultSidebar.transform.Find("InnerBox").Find("DrugHollowTypeButton").GetComponent<Button>();

        priceText = DefaultSidebar.transform.Find("InnerBox").Find("Price").GetComponent<Text>();
        nameText = DefaultSidebar.transform.Find("InnerBox").Find("NamePanel").Find("Name").GetComponent<Text>();
    }

    public void setupBar()
    {
        buyButton.onClick.AddListener(() => currentBuilding.setIsOwned(true));
        distilleryButton.onClick.AddListener(() => currentBuilding.setType("distillery"));
        casinoButton.onClick.AddListener(() => currentBuilding.setType("casino"));
        nightClubButton.onClick.AddListener(() => currentBuilding.setType("nightClub"));
        drugsButton.onClick.AddListener(() => currentBuilding.setType("drugHollow"));

        priceText.text = currentBuilding.getPrice().ToString();
        nameText.text = currentBuilding.transform.name;
    }


}
