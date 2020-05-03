using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool state = true;//true: drag, false: grid - sort of like can move
    bool prevState = false;//is this the first update to the new tile setting
    public GameObject controller;
    public GameObject gridTile;//the tile on the grid it is attached to

    // Update is called once per frame
    void Update()
    {
        stateSwitch();
        if (state)//switch - will be altered by the grid
        {
            Drag();
        }
        else
        {
            Grid();
        }
    }

    void stateSwitch()
    {
        if (gridTile == null)
        {
            state = true;
        }
        else
        {
            state = false;
        }
    }

    void Grid()//it is placed on a grid
    {
        if (prevState != state)//what to do when first grid
        {
            prevState = state;
            transform.position = gridTile.transform.position;//snap position to grid
            controller.GetComponent<Controller>().currentSelection = null;//remove from controller eye
        }
    }

    void Drag()//it is being dragged around
    {
        if (prevState != state)//what to do when first drag
        {
            prevState = state;
            controller.GetComponent<Controller>().currentSelection = this.gameObject;//add to controller eye
        }
        
        transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);//drag with mouse
        
        if (Input.GetMouseButtonUp(0))//dropped outside
        {
            Destroy(gameObject);
            controller.GetComponent<Controller>().currentSelection = null;//update that the user is no longer selecting something
        }
    }
}
