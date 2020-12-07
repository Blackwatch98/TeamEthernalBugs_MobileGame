using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBuildingScript : MonoBehaviour
{
    private float holdDownStartTime;
    private readonly float holdTimeLimit = 1f; // 1s
    private GameObject selectedObject;

    private BuildingClass building;

    //UI Elements
    private bool isUIMode = false;
    private GameObject PanelHeader;
    private GameObject DefaultSideBar;
    private GameObject ClassifiedSideBar;
    
    //Animations
    private HeaderAnim anim;
    private SliderMenuAnim anim2, anim3;

    void Start()
    {
        PanelHeader = GameObject.Find("PanelHeader");
        DefaultSideBar = GameObject.Find("DefaultSideBar");
        ClassifiedSideBar = GameObject.Find("ClassifiedSidebar");

        anim = PanelHeader.GetComponent<HeaderAnim>(); //get Header object
        anim2 = DefaultSideBar.GetComponent<SliderMenuAnim>(); //get DefaultSideBar object
        anim3 = ClassifiedSideBar.GetComponent<SliderMenuAnim>(); //get ClassifiedSideBar object
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            holdDownStartTime = Time.time;
        }

        if (Input.GetMouseButtonUp(0))
        {
            float holdDownTime = Time.time - holdDownStartTime;
            if (holdDownTime >= holdTimeLimit)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 100f))
                {
                    if (hit.transform && hit.transform.name != "Plane" && !isUIMode)
                    {
                        isUIMode = true;

                        print(hit.transform.gameObject.name);
                        selectedObject = hit.transform.gameObject;
                        building = selectedObject.GetComponent<BuildingClass>();

                        anim.ShowHideHeader();

                        if(!building.getIsOwned())
                            anim2.ShowHideMenu();
                        else
                            anim3.ShowHideMenu();
                    }
                }
                Debug.Log(holdDownTime + " tyle trzymałeś");
            }
            else 
            {
                anim.ShowHideHeader();
                isUIMode = false;
                if (!building.getIsOwned())
                    anim2.ShowHideMenu();
                else
                    anim3.ShowHideMenu();

                building = null;
            }
        }
    }

    public void fillSideBar(GameObject building)
    {

    }
}
