using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OwnedPanelScript
{
    private GameObject SideBar, TopSideBar, BlackMarketSideBar;
    private Text typeText;
    private Text nameText;
    private Text incomeText;
    private Button hireWorkersButton;
    private BuildingClass currentBuilding;

    public OwnedPanelScript(GameObject sidebar, BuildingClass building)
    {
        //currentBuilding.setNotOwnedPanel(null);

        currentBuilding = building;

        TopSideBar = GameObject.Find("Sidebar");
        BlackMarketSideBar = TopSideBar.transform.Find("BlackMarketSidebar").gameObject;

        Button blackMarketBackButton = BlackMarketSideBar.transform.Find("BackButton").GetComponent<Button>();
        blackMarketBackButton.onClick.AddListener(showHideBlackMarket);

        sidebar.SetActive(true);
        SideBar = GameObject.Instantiate(sidebar, sidebar.transform.parent.gameObject.transform);
        SideBar.gameObject.name = sidebar.gameObject.name + " " + currentBuilding.getName();
        sidebar.SetActive(false);

        Button blackMarketButton = SideBar.transform.Find("BlackMarketButton").GetComponent<Button>();
        blackMarketButton.onClick.AddListener(showHideBlackMarket);

        Debug.Log("Nowa instancja OwnedPanelscript dla obiektu " + currentBuilding.transform.name);
        typeText = SideBar.transform.Find("BuildingClass").GetComponent<Text>();
        nameText = SideBar.transform.Find("NamePanel").Find("Name").GetComponent<Text>();
        incomeText = SideBar.transform.Find("Income").GetComponent<Text>();
        hireWorkersButton = SideBar.transform.Find("HireWorkersButton").GetComponent<Button>();
    }

    public void setupBar()
    {
        typeText.text = currentBuilding.getType();
        nameText.text = currentBuilding.getName();
        incomeText.text = currentBuilding.getIncome().ToString();
        hireWorkersButton.onClick.AddListener(() => currentBuilding.raiseIncome());
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
    }

    private void showHideBlackMarket()
    {
        if (!BlackMarketSideBar.activeSelf)
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
