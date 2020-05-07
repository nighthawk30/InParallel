using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildButton : MonoBehaviour
{
    public GameObject controller;//simply triggers the controller to test for build
    public Sprite off;
    public Sprite on;

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Hover>().HoverTest())
        {
            GetComponent<Image>().sprite = on;
            if (Input.GetMouseButtonDown(0))//clicked
            {
                controller.GetComponent<Controller>().wireSwitch = false;//turn off wiring toggle
                controller.GetComponent<Controller>().BuildCircuit();
            }
        }
        else
        {
            GetComponent<Image>().sprite = off;
        }
    }
}
