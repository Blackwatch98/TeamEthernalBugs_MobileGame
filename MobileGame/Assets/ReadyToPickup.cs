using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading;
using System;

public class ReadyToPickup : MonoBehaviour
{
    int delayToPick = 5;
    double startTime, endTime, shiningStartTime, shiningEndTime;
    string clickedBuilding = "";
    object[] obj;
    List<Color> originalColors = new List<Color>();
    bool zazolcone = false;

    // Start is called before the first frame update

    void Start()
    {
        startTime = 0f;
        endTime = 0f;
        shiningStartTime = 0f;
        shiningEndTime = 0f;
    }

    // void is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool physicsRaycast = Physics.Raycast(ray, out hit);

        if (Equals(clickedBuilding, "") && Input.GetMouseButtonDown(0) && physicsRaycast)
        {
            originalColors = storeOriginalColor(hit.transform.name);
            startTime = Time.time;
            clickedBuilding = hit.transform.name;
        }

        if (clickedBuilding != "" && !Equals(checkIfColorChanged(clickedBuilding), "yellow"))
            endTime = Time.time;

        if (endTime - startTime > delayToPick && clickedBuilding != "")
        {

            //Koloruje zaznaczony budynek
            GameObject g = GameObject.Find(clickedBuilding);

            shiningStartTime = Time.time;

            foreach (var material in g.GetComponent<Renderer>().materials)
            {
                if (material.color[1] != 1) //jeśli nie zielone :P
                    material.color = new Color(1, 1, material.color[2], material.color[3]);
            }
            startTime = 0f;
            endTime = 0f;
        }

        if (clickedBuilding != "" && Equals(checkIfColorChanged(clickedBuilding), "yellow"))
            shiningEndTime = Time.time;

        if (shiningEndTime - shiningStartTime > 3.0f && clickedBuilding != "")
        {

            setOriginalColors(clickedBuilding, originalColors);

            shiningStartTime = 0f;
            shiningEndTime = 0f;

            clickedBuilding = "";
        }
    }

    void setOriginalColors(string gameObjectName, List<Color> originalColors)
    {
        GameObject gameObject = GameObject.Find(gameObjectName);
        int i = 0;
        foreach (var material in gameObject.GetComponent<Renderer>().materials)
        {
            material.color = originalColors[i];
            i++;
        }
    }

    List<Color> storeOriginalColor(string gameObjectName)
    {
        GameObject gameObject = GameObject.Find(gameObjectName);

        List<Color> originalColors = new List<Color>();

        foreach (var material in gameObject.GetComponent<Renderer>().materials)
        {
            originalColors.Add(material.color);
        }
        return originalColors;
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
}
