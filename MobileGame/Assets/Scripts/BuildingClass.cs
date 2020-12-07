using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingClass : MonoBehaviour
{
    public string name;
    public bool isOwned = false;
    public string type = null;
    public float price = 100;

    public bool getIsOwned()
    {
        return isOwned;
    }

    public void setIsOwned(bool value)
    {
        isOwned = value;
    }
}
