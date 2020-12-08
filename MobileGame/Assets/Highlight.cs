using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;
public class Highlight : MonoBehaviour
{
    int delayToPick = 5;
    int startProfitforAll = 0;
    int profitIncrease = 2;
    double startTime, endTime, countToYellow, shiningStartTime, shiningEndTime;
    string clickedBuilding = "";
    Camera cam;
    Transform clickedObject;
    object[] obj;
    List<Color> originalColorsOfSingleBuilding = new List<Color>();
    List<List<Color>> originalColorsOfAllBuildings;
    Text profitInfo;
    Text tobaccoCounter;
    bool countdownToYellowStarted = false;

    // Start is called before the first frame update

    void Start()
    {
        startTime = 0f;
        endTime = 0f;
        countToYellow = 0f;
        shiningStartTime = 0f;
        shiningEndTime = 0f;

        profitInfo = GameObject.Find("Przychód").GetComponent<Text>();
        tobaccoCounter = GameObject.Find("TobaccoCounter").GetComponent<Text>();

        obj = GameObject.FindObjectsOfType(typeof(GameObject));

        List<GameObject> doUsuniecia = new List<GameObject>();

        // int i = 0;
        foreach (GameObject gameObject in obj)
        {
            if (gameObject.name == "Main Camera" || gameObject.name == "Plane" || gameObject.name == "Area Light" || gameObject.name == "Canvas" || gameObject.name == "EventSystem" || gameObject.name == "Przychód" || gameObject.name == "TobaccoCounter")
            {
                doUsuniecia.Add(gameObject);
                //Debug.Log("Będę usuwać " + i);
            }
            //i++;
        }

        obj = obj.Except(doUsuniecia).ToArray();

        originalColorsOfAllBuildings = new List<List<Color>>();
        foreach (GameObject gameObject in obj)
        {
            List<Color> objectMaterials = new List<Color>();

            foreach (var material in gameObject.GetComponent<Renderer>().materials)
            {
                objectMaterials.Add(material.color);
            }
            originalColorsOfAllBuildings.Add(objectMaterials);
        }
    }

    // void is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        bool physicsRaycast = Physics.Raycast(ray, out hit);

        if (Equals(clickedBuilding, "") && Input.GetMouseButtonDown(0) && physicsRaycast)
        {
            originalColorsOfSingleBuilding = storeOriginalColor(hit.transform.name);
            startTime = Time.time;
            clickedBuilding = hit.transform.name;
            tobaccoCounter.transform.position = getClickedObjectPos(clickedBuilding);
        }

        if (Input.GetMouseButtonUp(0) && !countdownToYellowStarted)
            endTime = Time.time;

        if (endTime - startTime > 0.5f && !countdownToYellowStarted)
        {

            if (physicsRaycast && !Equals(clickedBuilding, ""))
            {

                setOrginalColorsForAllBuildings(clickedBuilding);

                highlightClicked("green", clickedBuilding);

                clickedBuilding = "";

            }
        }

        if (endTime - startTime < 0.5f && endTime - startTime > 0.0f)
        {

            if (clickedBuilding != "" && !Equals(checkIfColorChanged(clickedBuilding), "yellow"))
            {
                countToYellow = Time.time;
                countdownToYellowStarted = true;
            }

            if (countToYellow - startTime > delayToPick && clickedBuilding != "")
            {

                shiningStartTime = Time.time;

                highlightClicked("yellow", clickedBuilding);

                countToYellow = 0f;

                countdownToYellowStarted = false;

            }
        }

        if (countToYellow - startTime <= 0)
        {
            tobaccoCounter.text = "";
        }
        else
        {
            tobaccoCounter.text = (Math.Truncate(countToYellow - startTime)).ToString() + "s";
            Debug.Log("countToYellow "+ countToYellow);
            Debug.Log("startTime "+ startTime);
        }


        if (clickedBuilding != "" && Equals(checkIfColorChanged(clickedBuilding), "yellow"))
        {
            shiningEndTime = Time.time;
        }

        if (shiningEndTime - shiningStartTime > 3.0f && clickedBuilding != "")
        {
            setOriginalColors(clickedBuilding, originalColorsOfSingleBuilding);
            shiningStartTime = 0f;
            shiningEndTime = 0f;
            clickedBuilding = "";
            startProfitforAll = startProfitforAll + profitIncrease;
            profitInfo.text = "Przychód: " + startProfitforAll;
        }
    }

    void setOriginalColors(string gameObjectName, List<Color> originalColorsOfSingleBuilding)
    {
        GameObject gameObject = GameObject.Find(gameObjectName);
        int i = 0;
        foreach (var material in gameObject.GetComponent<Renderer>().materials)
        {
            material.color = originalColorsOfSingleBuilding[i];
            i++;
        }
    }

    void setOrginalColorsForAllBuildings(String clickedBuilding)
    {

        int i = 0;

        foreach (object o in obj)
        {
            GameObject gameObject2 = (GameObject)o;
            if (gameObject2.name != clickedBuilding)
            {

                setOriginalColors(gameObject2.name, originalColorsOfAllBuildings[i]);
                /*
                int j = 0;
                foreach (var material in gameObject2.GetComponent<Renderer>().materials)
                {
                    material.color = originalColorsOfAllBuildings[i][j];
                    j++;
                }
                */
            }
            i++;
        }
    }

    List<Color> storeOriginalColor(string gameObjectName)
    {
        GameObject gameObject = GameObject.Find(gameObjectName);

        List<Color> originalColorsOfSingleBuilding = new List<Color>();

        foreach (var material in gameObject.GetComponent<Renderer>().materials)
        {
            originalColorsOfSingleBuilding.Add(material.color);
        }
        return originalColorsOfSingleBuilding;
    }

    string checkIfColorChanged(string gameObjectName)
    {
        string returnColor;

        GameObject gameObject = GameObject.Find(gameObjectName);

        var material = gameObject.GetComponent<Renderer>().materials[0]; //wystarczy, że sprawdzimy pierwszy

        if (material.color[0] == 1 && material.color[1] < 1)
            returnColor = "red";
        else if (material.color[0] == 1 && material.color[1] == 1)
            returnColor = "yellow";
        else if (material.color[0] < 1 && material.color[1] == 1)
            returnColor = "green";
        else returnColor = "black";

        return returnColor;
    }

    Vector3 getClickedObjectPos(string clickedObjName)
    {
        GameObject gameObject = GameObject.Find(clickedObjName);
        cam = GetComponent<Camera>();
        return cam.WorldToScreenPoint(gameObject.transform.position);
    }

    void highlightClicked(String color, String clickedBuilding)
    {
        GameObject gameObject = GameObject.Find(clickedBuilding);

        foreach (var material in gameObject.GetComponent<Renderer>().materials)
        {
            if (Equals(color, "green") && material.color[0] != 1 && material.color[1] != 1)
            {
                material.color = new Color(material.color[0], 1, material.color[2], material.color[3]);

            }
            else if (Equals(color, "yellow") && material.color[1] != 1)
            {
                material.color = new Color(1, 1, material.color[2], material.color[3]);
            }
        }
    }
}
