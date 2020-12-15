using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectBuildingScript : MonoBehaviour
{
    private float holdDownStartTime;
    private readonly float holdTimeLimit = 1f; // 1s
    private GameObject selectedObject;

    private BuildingClass building;
    private bool holdMouseButtonFlag = false;

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

    void Start()
    {
        PanelHeader = GameObject.Find("PanelHeader");
        Debug.Log(PanelHeader);
        SideBar = GameObject.Find("Sidebar");
        DefaultSideBar = SideBar.transform.Find("DefaultSideBar").gameObject;
        ClassifiedSideBar = SideBar.transform.Find("ClassifiedSidebar").gameObject;
        BlackMarketSideBar = SideBar.transform.Find("BlackMarketSidebar").gameObject;

        Button blackMarketButton = ClassifiedSideBar.transform.Find("BlackMarketButton").GetComponent<Button>();
        blackMarketButton.onClick.AddListener(showHideBlackMarket);
        
        Button blackMarketBackButton = BlackMarketSideBar.transform.Find("BackButton").GetComponent<Button>();
        blackMarketBackButton.onClick.AddListener(showHideBlackMarket);

        anim = PanelHeader.GetComponent<HeaderAnim>(); //get Header object
        anim2 = SideBar.GetComponent<SliderMenuAnim>(); //get DefaultSideBar object
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isMouseOverUI())
        {
            holdDownStartTime = Time.time;
            holdMouseButtonFlag = true;
        }

        float holdDownTime = 0;
        
        if(holdMouseButtonFlag)
            holdDownTime = Time.time - holdDownStartTime;

        //UI activation
        if (holdDownTime > holdTimeLimit && !isUIMode && !isMouseOverUI())
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.transform && hit.transform.name != "Plane" && !isUIMode)
                {
                    isUIMode = true;

                    selectedObject = hit.transform.gameObject;
                    building = selectedObject.GetComponent<BuildingClass>();

                    anim.ShowHideHeader();

                    Debug.Log(building.getType());
                    if (building.getIsOwned() && building.getType() != "")
                    {
                        ClassifiedSideBar.SetActive(true);
                        OwnedPanelScript script = new OwnedPanelScript(ClassifiedSideBar, building);
                        script.setupBar();
                    }
                    else
                    {
                        ClassifiedSideBar.SetActive(false);
                        NotOwnedPanelScript script = new NotOwnedPanelScript(DefaultSideBar, building);
                        script.setupBar();
                    }
                    
                    anim2.ShowHideMenu();
                }
            }
            //Debug.Log(holdDownTime + " tyle trzymałeś");
        }
        
        //cancel ui panel
        if(Input.GetMouseButtonDown(0) && isUIMode && !isMouseOverUI())
        {
            anim.ShowHideHeader();
            isUIMode = false;

            anim2.ShowHideMenu();

            holdMouseButtonFlag = false;
            building = null;
        }
    }

    private bool isMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    private void showHideBlackMarket()
    {
        if(!BlackMarketSideBar.activeSelf)
            BlackMarketSideBar.SetActive(true);
        else
            BlackMarketSideBar.SetActive(false);
    }

    private void showHideClassifiedSidebar()
    {
        if (!ClassifiedSideBar.activeSelf)
            ClassifiedSideBar.SetActive(true);
        else
            ClassifiedSideBar.SetActive(false);
    }
}
