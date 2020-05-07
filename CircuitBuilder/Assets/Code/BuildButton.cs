using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildButton : MonoBehaviour
{
    public GameObject controller;//simply triggers the controller to test for build

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && GetComponent<TileHover>().HoverTest())//clicked
        {
            controller.GetComponent<Controller>().wireSwitch = false;//turn off wiring toggle
            controller.GetComponent<Controller>().BuildCircuit();
        }
    }
}
