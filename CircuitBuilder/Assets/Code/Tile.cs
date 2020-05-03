using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    int state = 0;//0: drag, 1: grid - sort of like can move, 2: wires
    int prevState = -1;//lets us run a function once upon entering a new state
    public GameObject controller;
    public GameObject gridTile;//the tile on the grid it is attached to
    public GameObject wire1;//max of 2 wires - should point to wire which points to another object - on click 2
    public GameObject wire2;

    // Update is called once per frame
    void Update()
    {
        if (controller.GetComponent<Controller>().wireToggle)
        {
            state = 2;
            Wires();
        }
        else if (gridTile != null)
        {
            state = 1;
            Grid();
        }
        else
        {
            state = 0;
            Drag();
        }
    }

    void Wires()
    {
        if (Input.GetMouseButtonDown(0) && GetComponent<TileHover>().HoverTest())//clicked with wires
        {
            if (controller.GetComponent<Controller>().wireConnection == null)//first click with wires
            {
                //make a wire and set that wire in the controller and set this object as the first
            }
            else//there already is a wire
            {
                //controller.GetComponent<Controller>().wireConnection
                //set this as the wire second, set the wire as one of the wires and remove the wire from the controller
            }
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
            //delete the wire and the reference to the wire from the other connected object
            controller.GetComponent<Controller>().currentSelection = null;//update that the user is no longer selecting something
        }
    }
}
