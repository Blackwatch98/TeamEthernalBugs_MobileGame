using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingClass : MonoBehaviour
{
    public GameObject specifiedBuilding;
    public bool isOwned = false;
    public string type = null;
    public float price;
    public float income;
    public float workerIncomeIncrease;
    public int maxNumOfWorkers;


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
}
