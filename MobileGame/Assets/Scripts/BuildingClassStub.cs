using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BuildingClassStub
{
   public GameObject specifiedBuilding;
   public bool isOwned = false;
   public string type = null;
   public float price;
   public float income;
   public float workerIncomeIncrease;
   public int maxNumOfWorkers;
   public OwnedPanelScript ownedPanelScript = null;
   public NotOwnedPanelScript notOwnedPanelScript = null;
   public string buildingName;
   public Sprite buildingImage;
   public float startWorkerCost;
}
