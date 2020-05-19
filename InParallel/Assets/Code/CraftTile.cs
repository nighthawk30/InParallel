using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftTile : MonoBehaviour
{
    public GameObject controller;
    public Sprite craft;

    // Update is called once per frame
    void Update()
    {
        if (controller.GetComponent<Controller>().output != null)
        {
            GetComponent<Image>().sprite = controller.GetComponent<Controller>().output.GetComponent<Image>().sprite;//GetComponent<Image>().sprite
        }
        else
        {
            GetComponent<Image>().sprite = craft;
        }
    }
}
