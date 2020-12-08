using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OwnedPanelScript
{
    private GameObject ClassifiedSideBar;
    private Text typeText;
    private Text nameText;
    private Text incomeText;
    private Button hireWorkersButton;
    private BuildingClass currentBuilding;

    public OwnedPanelScript(GameObject sidebar, BuildingClass building)
    {
        ClassifiedSideBar = sidebar;
        currentBuilding = building;

        typeText = ClassifiedSideBar.transform.Find("InnerBox").Find("BuildingClass").GetComponent<Text>();
        nameText = ClassifiedSideBar.transform.Find("InnerBox").Find("NamePanel").Find("Name").GetComponent<Text>();
        incomeText = ClassifiedSideBar.transform.Find("InnerBox").Find("Income").GetComponent<Text>();
        hireWorkersButton = ClassifiedSideBar.transform.Find("InnerBox").Find("HireWorkersButton").GetComponent<Button>();
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
