using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;

public class Highlight : MonoBehaviour
{
    int delayToPick = 5;
    float startProfitforAll;
    float profitIncrease;
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

    //UI Elements
    private bool isUIMode = false;
    private GameObject PanelHeader;
    private GameObject SideBar;
    private GameObject DefaultSideBar;
    private GameObject ClassifiedSideBar;
    private GameObject BlackMarketSideBar;

    //Animations
    private HeaderAnim anim;
    private SliderMenuAnim activeSlider;
    private SliderMenuAnim anim2;

    // Start is called before the first frame update

    void Start()
    {
        startTime = 0f;
        endTime = 0f;
        countToYellow = 0f;
        shiningStartTime = 0f;
        shiningEndTime = 0f;
        //tworzymy instancje klasy budynek
        //BuildingClass b = new BuildingClass();
        //profitIncrease = b.income;

        //PanelHeader = GameObject.Find("PanelHeader");

        SideBar = GameObject.Find("Sidebar");
        DefaultSideBar = SideBar.transform.Find("DefaultSideBar").gameObject;
        ClassifiedSideBar = SideBar.transform.Find("ClassifiedSidebar").gameObject;
        
        //blackMarketButton.onClick.AddListener(showHideBlackMarket);

    
        //blackMarketBackButton.onClick.AddListener(showHideBlackMarket);

        //anim = PanelHeader.GetComponent<HeaderAnim>(); //get Header object
        anim2 = SideBar.GetComponent<SliderMenuAnim>(); //get DefaultSideBar object

        profitInfo = GameObject.Find("gold").GetComponent<Text>();
        tobaccoCounter = GameObject.Find("tobaccoCounter").GetComponent<Text>();

        obj = GameObject.FindGameObjectsWithTag("Handleable");

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

        if (Equals(clickedBuilding, "") && Input.GetMouseButtonDown(0) && physicsRaycast && obj.Contains(GameObject.Find(hit.transform.name)) && !isUIMode)
        {
            originalColorsOfSingleBuilding = storeOriginalColor(hit.transform.name);
            startTime = Time.time;
            clickedBuilding = hit.transform.name;
            tobaccoCounter.transform.position = getClickedObjectPos(clickedBuilding);
            //anim.ShowHideHeader(clickedBuilding);
            if (SideBar.GetComponent<SliderMenuAnim>().getState())
            {
                anim2.ShowHideMenu(clickedBuilding);
            }
        }

        if (Input.GetMouseButtonUp(0) && !countdownToYellowStarted)
            endTime = Time.time;

        if (endTime - startTime > 0.5f && !countdownToYellowStarted)
        {

            if (physicsRaycast && !Equals(clickedBuilding, "") && !isUIMode)
            {
             
                setOrginalColorsForAllBuildings(clickedBuilding);

                highlightClicked("green", clickedBuilding);

                BuildingClass building = GameObject.Find(clickedBuilding).GetComponent<BuildingClass>();

                if (building.getIsOwned())
                {
                    if (building.getOwnedPanel() == null)
                    {
                        building.getNotOwnedPanel().getSidebar().SetActive(true);
                        building.getNotOwnedPanel().setupBar();
                    }
                    else
                    {
                        building.getOwnedPanel().getSidebar().SetActive(true);
                        building.getOwnedPanel().setupBar();
                    }         
                }
                // jeżeli wybrany budynek nie jest owned to obsługujemy panel notowned
                else
                {
                    // jeżeli wybrany budynek nie ma panelu notowned to go tworzymy i ustawiamy pola z nazwą itp.
                    if (building.getNotOwnedPanel()==null)
                    {
                        Debug.Log("Wybrany budynek nie ma panelu not owned" + building.getName());
                        NotOwnedPanelScript script = new NotOwnedPanelScript(DefaultSideBar, building);
                        building.setNotOwnedPanel(script);
                        building.getNotOwnedPanel().setupBar();
                    }
                    // jeżeli wybrany budynek ma panel notowned to go aktywujemy i ustawiamy pola z nazwą itp.
                    else
                    {
                        Debug.Log("Wybrany budynek ma panel not owned" + building.getName());
                        building.getNotOwnedPanel().getSidebar().SetActive(true);
                        building.getNotOwnedPanel().setupBar();
                    }
                    // jeżeli dany budynek ma panel owned to go ukrywamy
                    if (building.getOwnedPanel() != null)
                    {
                        Debug.Log("Wybrany budynek ma panel owned" + building.getName());
                        building.getOwnedPanel().getSidebar().SetActive(false);
                    }
                }
                //Deaktywacja bocznych paneli dla wszystkich niezaznaczonych
                foreach (GameObject gameObject in obj)
                {
                    if (building.name != gameObject.name && gameObject.GetComponent<BuildingClass>().getNotOwnedPanel()!=null)
                    {
                        gameObject.GetComponent<BuildingClass>().getNotOwnedPanel().getSidebar().SetActive(false);
                    }
                    if (building.name != gameObject.name && gameObject.GetComponent<BuildingClass>().getOwnedPanel() != null)
                    {
                        gameObject.GetComponent<BuildingClass>().getOwnedPanel().getSidebar().SetActive(false);
                    }
                }

                anim2.ShowHideMenu(clickedBuilding);

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
            Debug.Log("countToYellow " + countToYellow);
            Debug.Log("startTime " + startTime);
        }


        if (clickedBuilding != "" && Equals(checkIfColorChanged(clickedBuilding), "yellow"))
        {
            shiningEndTime = Time.time;
        }

        if (shiningEndTime - shiningStartTime > 3.0f && clickedBuilding != "")
        {
            setOriginalColors(clickedBuilding, originalColorsOfSingleBuilding);
            //anim.ShowHideHeader(clickedBuilding);
            shiningStartTime = 0f;
            shiningEndTime = 0f;

            Debug.Log("Wszedłem we ifa");
            Debug.Log("Wartość zysku");
            Debug.Log(GameObject.Find(clickedBuilding).GetComponent<BuildingClass>().income);

            startProfitforAll = startProfitforAll + GameObject.Find(clickedBuilding).GetComponent<BuildingClass>().income;
            profitInfo.text =  startProfitforAll.ToString();
            clickedBuilding = "";
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
                //anim2.ShowHideMenu();

            }
            else if (Equals(color, "yellow") && material.color[1] != 1)
            {
                material.color = new Color(1, 1, material.color[2], material.color[3]);
            }
        }
    }
}
