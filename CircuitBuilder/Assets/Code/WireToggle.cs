using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class WireToggle : MonoBehaviour
{
    public GameObject controller;// a variation of the selector icon
    public Sprite wireOn;
    public Sprite wireOff;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && GetComponent<TileHover>().HoverTest())//it was clicked
        {
            controller.GetComponent<Controller>().wireSwitch = !controller.GetComponent<Controller>().wireSwitch;//toggle wire toggle
        }

        if (controller.GetComponent<Controller>().wireSwitch)//wires is on
        {
            GetComponent<Image>().sprite = wireOn;
        }
        else
        {
            GetComponent<Image>().sprite = wireOff;
        }
    }
}
