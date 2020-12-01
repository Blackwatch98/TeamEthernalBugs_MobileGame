using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBuildingScript : MonoBehaviour
{
    private float holdDownStartTime;
    private readonly float holdTimeLimit = 1f; // 1s
    private GameObject selectedObject;

    void Start()
    {

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
                    if (hit.transform && hit.transform.gameObject.name != "Plane")
                    {
                        print(hit.transform.gameObject.name);

                    }
                }
                Debug.Log(holdDownTime + " tyle trzymałeś");
            }
        }
    }
}
