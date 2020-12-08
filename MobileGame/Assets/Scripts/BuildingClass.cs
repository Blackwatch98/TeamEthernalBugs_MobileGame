using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingClass : MonoBehaviour
{
    public bool isOwned = false;
    public string type = null;
    public float price = 100;
    public float income = 1000;
    public float workerIncomeIncrease = 100;

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
