using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;
using System.IO;
using TMPro;

public class Highlight : MonoBehaviour
{
    int delayToPick = 5;
    public static int hireMultiplier = 2;
    public static float startProfitforAll;
    float profitIncrease;
   public static double startTime, startTimeFading, endTimeFading, endTime, countToYellow, shiningStartTime, shiningEndTime, endTimeGreen;
    string clickedBuilding = "";
    Camera cam;
    Transform clickedObject;
    object[] obj;
    List<Color> originalColorsOfSingleBuilding = new List<Color>();
    List<List<Color>> originalColorsOfAllBuildings;
    public static TextMeshProUGUI profitInfo, counterText, lossIndicator;
    bool countdownToYellowStarted = false;

    //UI Elements
    private bool isUIMode = false;
    private GameObject PanelHeader;
    private GameObject SideBar;
    private GameObject DefaultSideBar;
    private GameObject BlackMarketSideBar;
    private Button blackMarketBackButton;

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
        endTimeGreen = 0f;
        shiningStartTime = 0f;
        shiningEndTime = 0f;
    

        //PanelHeader = GameObject.Find("PanelHeader");

        SideBar = GameObject.Find("Sidebar");
        DefaultSideBar = SideBar.transform.Find("DefaultSideBar").gameObject;

        //anim = PanelHeader.GetComponent<HeaderAnim>(); //get Header object
        anim2 = SideBar.GetComponent<SliderMenuAnim>(); //get DefaultSideBar object

        profitInfo = GameObject.Find("gold").GetComponent<TextMeshProUGUI>();
        lossIndicator = GameObject.Find("LossIndicator").GetComponent<TextMeshProUGUI>();

        counterText = GameObject.Find("counterText").GetComponent<TextMeshProUGUI>();

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

        //SHARED PREFS
        if (PlayerPrefs.GetFloat("playerCoinsWealth") != null)
        {
            startProfitforAll = PlayerPrefs.GetFloat("playerCoinsWealth");
            profitInfo.text = startProfitforAll.ToString();
        }

         var components = GameObject.FindObjectsOfType<BuildingClass>();
         var jsonString = File.ReadAllText("building_classes.json");
         BuildingClassStub[] buildingClasses = JsonHelper.FromJson<BuildingClassStub>(jsonString);
         foreach (var buildingClassStub in buildingClasses)
         {
            foreach (var component in components)
            {
               if (buildingClassStub.buildingName == ((BuildingClass)component).buildingName)
               {
                  Debug.Log("Deserialize of building: " + buildingClassStub.buildingName);
                  ((BuildingClass)component).Deserialize(buildingClassStub);
               }
            }
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
            counterText.transform.position = getClickedObjectPos(clickedBuilding);
            //anim.ShowHideHeader(clickedBuilding);
            if (SideBar.GetComponent<SliderMenuAnim>().getState())
            {
                anim2.ShowHideMenu(clickedBuilding);
            }
        }

        if (Input.GetMouseButtonUp(0) && !countdownToYellowStarted)
            endTime = Time.time;

        if (!countdownToYellowStarted)
            endTimeGreen = Time.time;


        if (endTimeGreen - startTime > 0.5f && Input.GetMouseButton(0) && !countdownToYellowStarted)
        {
            
            if (physicsRaycast && !Equals(clickedBuilding, "") && !isUIMode)
            {

                setOrginalColorsForAllBuildings(clickedBuilding);

                highlightClicked("green", clickedBuilding);

                BuildingClass building = GameObject.Find(clickedBuilding).GetComponent<BuildingClass>();

                makeUI(building);

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
            counterText.text = "";
        }
        else
        {
            counterText.text = (Math.Truncate(countToYellow - startTime)).ToString() + "s";
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

            startProfitforAll = startProfitforAll + GameObject.Find(clickedBuilding).GetComponent<BuildingClass>().income;
            profitInfo.text =  startProfitforAll.ToString();
            clickedBuilding = "";
            Debug.Log(startProfitforAll);
            //SHARED PREFS
            PlayerPrefs.SetFloat("playerCoinsWealth", startProfitforAll);
        }

        endTimeFading = Time.time;

        if (endTimeFading-startTimeFading >= 2.0f)
        {
            lossIndicator.text = " ";
        }

    }

    private void makeUI(BuildingClass building)
    {
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
            if (building.getNotOwnedPanel() == null)
            {
                NotOwnedPanelScript script = new NotOwnedPanelScript(DefaultSideBar, building);
                building.setNotOwnedPanel(script);
                building.getNotOwnedPanel().setupBar();
            }
            // jeżeli wybrany budynek ma panel notowned to go aktywujemy i ustawiamy pola z nazwą itp.
            else
            {
                building.getNotOwnedPanel().getSidebar().SetActive(true);
                building.getNotOwnedPanel().setupBar();
            }
            // jeżeli dany budynek ma panel owned to go ukrywamy
            if (building.getOwnedPanel() != null)
            {
                building.getOwnedPanel().getSidebar().SetActive(false);
            }
        }
        //Deaktywacja bocznych paneli dla wszystkich niezaznaczonych
        foreach (GameObject gameObject in obj)
        {
            if (building.name != gameObject.name && gameObject.GetComponent<BuildingClass>().getNotOwnedPanel() != null)
            {
                gameObject.GetComponent<BuildingClass>().getNotOwnedPanel().getSidebar().SetActive(false);
            }
            if (building.name != gameObject.name && gameObject.GetComponent<BuildingClass>().getOwnedPanel() != null)
            {
                gameObject.GetComponent<BuildingClass>().getOwnedPanel().getSidebar().SetActive(false);
            }
        }

        anim2.ShowHideMenu(clickedBuilding);
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

    void hideBlackMarket(BuildingClass building)
    {
        Debug.Log("Chowam blackmarket "+ building.getName());
        BlackMarketSideBar.SetActive(false);
        building.getOwnedPanel().getSidebar().SetActive(true);
    }

   public static void decreaseIncome(BuildingClass building)
   {
      startProfitforAll -= building.getStartWorkerCost();
      profitInfo.text = startProfitforAll.ToString();
      startTimeFading = Time.time;
      lossIndicator.text = "-" + building.getStartWorkerCost();
       

      float zm = building.getStartWorkerCost() * hireMultiplier;
      Debug.Log("ZM " + zm);
      building.setStartWorkerCost(zm);
   }

   void OnDestroy()
   {
      var components = GameObject.FindObjectsOfType<BuildingClass>();
      List<BuildingClassStub> buildingClassesList = new List<BuildingClassStub>();
      var sr = File.CreateText("building_classes.json");
      foreach (var component in components)
      {
         //string json = JsonUtility.ToJson(component.Serialize(), true);
         //sr.WriteLine(json);
         buildingClassesList.Add(((BuildingClass)component).Serialize());
      }
      BuildingClassStub[] buildingClasses = buildingClassesList.ToArray();
      string json = JsonHelper.ToJson(buildingClasses, true);
      sr.WriteLine(json);
      sr.Close();
   }
}
