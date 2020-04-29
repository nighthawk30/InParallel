using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragTile : MonoBehaviour//only serves to show the user where the tile is, otherwise redundant
{
    Vector3 cursorLocation = new Vector3(0, 0, 0);
    float screenRatio;
    public GameObject controller;

    // Start is called before the first frame update
    void Start()
    {
        screenRatio = 160 / ((float)(Screen.height));
    }

    // Update is called once per frame
    void Update()
    {
        //modified version of hovertile class to tp the tile to the mouse
        cursorLocation.x = (Input.mousePosition.x - Screen.width / 2) * screenRatio;
        cursorLocation.y = (Input.mousePosition.y - Screen.height / 2) * screenRatio;
        transform.position = cursorLocation;

        if (Input.GetMouseButtonUp(0))
        {
            Destroy(gameObject);
            controller.GetComponent<Controller>().currentSelection = null;//update that the user is no longer selecting something
        }
    }
}