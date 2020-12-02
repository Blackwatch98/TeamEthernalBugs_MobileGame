using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Highlight : MonoBehaviour
{
    double startTime;
    double endTime;
    Transform clickedObject;
    object[] obj;
    List<List<Color>> originalColors;

    // Start is called before the first frame update

    void Start()
    {
        startTime = 0f;
        endTime = 0f;
        obj = GameObject.FindObjectsOfType(typeof(GameObject));

        List<GameObject> doUsuniecia = new List<GameObject>();

        foreach (GameObject gameObject in obj)
        {
            Debug.Log(gameObject.name);
        }

       // int i = 0;
        foreach (GameObject gameObject in obj)
        {
            if (gameObject.name == "Main Camera" || gameObject.name == "Plane" || gameObject.name == "Area Light")
            {  
                doUsuniecia.Add(gameObject);
                //Debug.Log("Będę usuwać " + i);
            }
            //i++;
        }

        obj = obj.Except(doUsuniecia).ToArray();

        foreach (GameObject gameObject in obj)
        {
            Debug.Log(gameObject.name);
        }

        Debug.Log("Start");

        originalColors = new List<List<Color>>();
        foreach (GameObject gameObject in obj)
        {

                Debug.Log(gameObject.name);
                List<Color> objectMaterials = new List<Color>();

                foreach (var material in gameObject.GetComponent<Renderer>().materials)
                {
                    objectMaterials.Add(material.color);
                    Debug.Log(material.color.ToString());
                }
                originalColors.Add(objectMaterials);
        }
    }

    // void is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
            startTime = Time.time;
        if (Input.GetMouseButtonUp(0))
            endTime = Time.time;
        if (endTime - startTime > 0.5f)
        {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if ((Physics.Raycast(ray, out hit)))
            {
                int i = 0;
                foreach (object o in obj)
                {
                    GameObject gameObject = (GameObject)o;
                    if (gameObject.name != hit.transform.name)
                    {
                        Debug.Log("Koloruję na biało obiekt " + gameObject.name);
                        int j = 0;
                        foreach(var material in gameObject.GetComponent<Renderer>().materials)
                        {
                            Debug.Log(originalColors[i][j].ToString() + "i: "+i + "j: "+j);
                            material.color = originalColors[i][j];
                            j++;
                        }
                        Debug.Log("licba j: " + j);
                    }
                    i++;
                }
                Debug.Log("licba i: " + i);

                if (hit.transform.name != "Plane")
                {
                    foreach(var hitmaterial in hit.transform.GetComponent<Renderer>().materials)
                    {
                        hitmaterial.color=new Color(hitmaterial.color[0],1, hitmaterial.color[2], hitmaterial.color[3]);
                    }
                }

                GameObject g = GameObject.Find("NameOfGameObject");

                Debug.Log("Teraz kliknięty " + hit.transform.name);

                clickedObject = hit.transform;

            }

            startTime = 0f;
            endTime = 0f;
        }
    }
}
