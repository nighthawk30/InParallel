using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragTile : MonoBehaviour//only serves to show the user where the tile is, otherwise redundant
{
    Vector3 cursorLocation = new Vector3(0, 0, 0);
    public GameObject controller;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //tp tile to mouse
        cursorLocation.x = Input.mousePosition.x;
        cursorLocation.y = Input.mousePosition.y;
        transform.position = cursorLocation;

        if (Input.GetMouseButtonUp(0))
        {
            Destroy(gameObject);
            controller.GetComponent<Controller>().currentSelection = null;//update that the user is no longer selecting something
        }
    }
}