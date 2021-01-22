
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;

public class BuildingClass : MonoBehaviour
{
    public GameObject specifiedBuilding;
    public bool isOwned = false;
    public string type = null;
    public float price;
    public float income;
    public float workerIncomeIncrease;
    public int maxNumOfWorkers;
    public OwnedPanelScript ownedPanelScript=null;
    public NotOwnedPanelScript notOwnedPanelScript=null;
    public string buildingName;
    public Sprite buildingImage;
    public float startWorkerCost;


    public BuildingClass(GameObject specifiedBuilding, bool isOwned, string type, float price, float income, float workerIncomeIncrease, int maxNumOfWorkers)
    {
        this.specifiedBuilding = specifiedBuilding;
        this.isOwned = isOwned;
        this.type = type;
        this.price = price;
        this.income = income;
        this.workerIncomeIncrease = workerIncomeIncrease;
        this.maxNumOfWorkers = maxNumOfWorkers;
    }

    public bool getIsOwned()
    {
        return isOwned;
    }

    public void setIsOwned(bool value)
    {
        isOwned = value;
    }

    public float getPrice()
    {
        return price;
    }

    public void setType(string input)
    {
        type = input;
    }

    public string getType()
    {
        return type;
    }

    public float getIncome()
    {
        return income;
    }

    public void raiseIncome()
    {
        income += workerIncomeIncrease;
    }



    public void setNotOwnedPanel(NotOwnedPanelScript input)
    {
        notOwnedPanelScript = input;
    }

    public void setOwnedPanel(OwnedPanelScript input)
    {
        ownedPanelScript = input;
    }

    public OwnedPanelScript getOwnedPanel()
    {
        return ownedPanelScript;
    }

    public NotOwnedPanelScript getNotOwnedPanel()
    {
        return notOwnedPanelScript;
    }

    public void setName(string newName)
    {
        this.buildingName = newName;
    }

    public string getName()
    {
        return buildingName;
    }
    public Sprite getImage()
    {
        return buildingImage;
    }

    public float getStartWorkerCost()
    {
        return startWorkerCost;
    }

    public void setStartWorkerCost(float input)
    {
        startWorkerCost = input;
    }

    public void Deserialize(BuildingClassStub buildingClassStub)
    {
      this.specifiedBuilding = buildingClassStub.specifiedBuilding;
      this.isOwned = buildingClassStub.isOwned;
      this.type = buildingClassStub.type;
      this.price = buildingClassStub.price;
      this.income = buildingClassStub.income;
      this.workerIncomeIncrease = buildingClassStub.workerIncomeIncrease;
      this.maxNumOfWorkers = buildingClassStub.maxNumOfWorkers;
      this.ownedPanelScript = buildingClassStub.ownedPanelScript;
      this.notOwnedPanelScript = buildingClassStub.notOwnedPanelScript;
      this.buildingName = buildingClassStub.buildingName;
      this.buildingImage = buildingClassStub.buildingImage;
      this.startWorkerCost = buildingClassStub.startWorkerCost;
   }

   public BuildingClassStub Serialize()
   {
      BuildingClassStub buildingClassStub = new BuildingClassStub();

      buildingClassStub.specifiedBuilding = this.specifiedBuilding;
      buildingClassStub.isOwned = this.isOwned;
      buildingClassStub.type = this.type;
      buildingClassStub.price = this.price;
      buildingClassStub.income = this.income;
      buildingClassStub.workerIncomeIncrease = this.workerIncomeIncrease;
      buildingClassStub.maxNumOfWorkers = this.maxNumOfWorkers;
      buildingClassStub.ownedPanelScript = this.ownedPanelScript;
      buildingClassStub.notOwnedPanelScript = this.notOwnedPanelScript;
      buildingClassStub.buildingName = this.buildingName;
      buildingClassStub.buildingImage = this.buildingImage;
      buildingClassStub.startWorkerCost = this.startWorkerCost;

      return buildingClassStub;
   }
}
