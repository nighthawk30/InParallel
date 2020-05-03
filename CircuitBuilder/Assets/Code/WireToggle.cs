using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class WireToggle : MonoBehaviour
{
    public GameObject controller;// a variation of the selector icon

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && GetComponent<TileHover>().HoverTest())//it was clicked
        {
            controller.GetComponent<Controller>().wireToggle = !controller.GetComponent<Controller>().wireToggle;//toggle wire toggle
            if (controller.GetComponent<Controller>().wireToggle)//wires is on
            {
                //switch sprite to toggle on
            }
            else
            {
                //switch sprite to toggle off
            }

        }
    }
}
