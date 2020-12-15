using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OwnedPanelScript
{
    private GameObject SideBar;
    private Text typeText;
    private Text nameText;
    private Text incomeText;
    private Button hireWorkersButton;
    private BuildingClass currentBuilding;

    public OwnedPanelScript(GameObject sidebar, BuildingClass building)
    {
        SideBar = sidebar;
        currentBuilding = building;

        typeText = SideBar.transform.Find("BuildingClass").GetComponent<Text>();
        nameText = SideBar.transform.Find("NamePanel").Find("Name").GetComponent<Text>();
        incomeText = SideBar.transform.Find("Income").GetComponent<Text>();
        hireWorkersButton = SideBar.transform.Find("HireWorkersButton").GetComponent<Button>();
    }

    public void setupBar()
    {
        typeText.text = currentBuilding.getType();
        nameText.text = currentBuilding.transform.name;
        incomeText.text = currentBuilding.getIncome().ToString();
        hireWorkersButton.onClick.AddListener(() => currentBuilding.raiseIncome());
        hireWorkersButton.onClick.AddListener(() => updateBar());
    }

    public void updateBar()
    {
        incomeText.text = currentBuilding.getIncome().ToString();
    }
}
