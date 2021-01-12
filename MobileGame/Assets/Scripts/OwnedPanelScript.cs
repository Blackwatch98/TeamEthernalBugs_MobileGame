using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OwnedPanelScript
{
    private GameObject SideBar, TopSideBar, BlackMarketSideBar;
    private Text typeText;
    private Text nameText;
    private Text incomeText, lossText;
    private Button hireWorkersButton, blackMarketBackButton;
    private BuildingClass currentBuilding;
    private Image buildingImage;

    

    public OwnedPanelScript(GameObject sidebar, BuildingClass building)
    {
        //currentBuilding.setNotOwnedPanel(null);

        currentBuilding = building;

        TopSideBar = GameObject.Find("Sidebar");
        BlackMarketSideBar = TopSideBar.transform.Find("BlackMarketSidebar").gameObject;

        sidebar.SetActive(true);
        SideBar = GameObject.Instantiate(sidebar, sidebar.transform.parent.gameObject.transform);
        SideBar.gameObject.name = sidebar.gameObject.name + " " + currentBuilding.getName();
        sidebar.SetActive(false);

        Button blackMarketButton = SideBar.transform.Find("BlackMarketButton").GetComponent<Button>();
        blackMarketButton.onClick.AddListener(showHideBlackMarket);

        BlackMarketSideBar = GameObject.Instantiate(TopSideBar.transform.Find("BlackMarketSidebar").gameObject, TopSideBar.transform);
        BlackMarketSideBar.gameObject.name = TopSideBar.transform.Find("BlackMarketSidebar").gameObject.name + " " + currentBuilding.getName();
        blackMarketBackButton = BlackMarketSideBar.transform.Find("BackButton").GetComponent<Button>();
        blackMarketBackButton.onClick.AddListener(showHideBlackMarket);

        typeText = SideBar.transform.Find("BuildingClass").GetComponent<Text>();
        nameText = SideBar.transform.Find("NamePanel").Find("Name").GetComponent<Text>();
        incomeText = SideBar.transform.Find("Income").GetComponent<Text>();
        lossText = SideBar.transform.Find("LossInfo").GetComponent<Text>();
        hireWorkersButton = SideBar.transform.Find("HireWorkersButton").GetComponent<Button>();

        buildingImage = SideBar.transform.Find("ObjectImage").GetComponent<Image>();
    }

    public void setupBar()
    {
        typeText.text = currentBuilding.getType();
        nameText.text = currentBuilding.getName();
        incomeText.text = currentBuilding.getIncome().ToString();
        lossText.text = currentBuilding.getStartWorkerCost().ToString();
        buildingImage.sprite = currentBuilding.getImage();

        hireWorkersButton.onClick.AddListener(() => currentBuilding.raiseIncome());
        hireWorkersButton.onClick.AddListener(() => Highlight.decreaseIncome(currentBuilding));
        hireWorkersButton.onClick.AddListener(() => updateBar());
    }

    public GameObject getSidebar()
    {
        return SideBar;
    }

    public void setSidebar(GameObject input)
    {
        SideBar = input;
    }

    public void updateBar()
    {
        
        incomeText.text = currentBuilding.getIncome().ToString();

        lossText.text = currentBuilding.getStartWorkerCost().ToString();
        
        Debug.Log("Start worker cost " + currentBuilding.getStartWorkerCost());
        Debug.Log("Mnoże razy " + Highlight.hireMultiplier);
    }

    private void showHideBlackMarket()
    {
        if(!BlackMarketSideBar.activeSelf)
        {
            BlackMarketSideBar.SetActive(true);
            SideBar.SetActive(false);
        }
        else
        {
            BlackMarketSideBar.SetActive(false);
            SideBar.SetActive(true);
        }
    }
}
