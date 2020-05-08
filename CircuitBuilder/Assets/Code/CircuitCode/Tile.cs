using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject wire;
    int state = 0;//0: drag, 1: grid - sort of like can move, 2: wires
    int prevState = -1;//lets us run a function once upon entering a new state
    public GameObject controller;
    public GameObject gridTile;//the tile on the grid it is attached to
    GameObject black;//the thing I use to shift variables around in obj
    public GameObject red;//max of 2 wires - should point to wire which points to another object - on click 2
    public GameObject blue;
    public int tileType;//this is set externally

    // Update is called once per frame
    void Update()
    {
        if (controller.GetComponent<Controller>().wireSwitch)
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
        if (Input.GetMouseButtonDown(0) && GetComponent<Hover>().HoverTest() && (red == null || blue == null))//clicked with wires and there are more connections on this object - for now
        {
            if (controller.GetComponent<Controller>().wireConnection == null)//first click with wires
            {
                black = Instantiate(wire, transform.position, transform.rotation, transform.parent);//create it
                black.GetComponent<RectTransform>().sizeDelta = GetComponent<RectTransform>().sizeDelta;//give it the proper size
                black.GetComponent<Wire>().controller = controller;//give it the controller
                black.GetComponent<Wire>().element1 = this.gameObject;//tell  it this is the first element it is connected to
                controller.GetComponent<Controller>().wireConnection = black;//set the controller for the next part

                if (red == null)
                {
                    red = black;
                }
                else
                {
                    blue = black;
                }
            }
            else//there already is a wire ADD AN IF THE WIRE THAT IS THERE IS THIS ONE, in that case, kill the wire
            {
                black = controller.GetComponent<Controller>().wireConnection;
                black.GetComponent<Wire>().element2 = this.gameObject;//it could only ever be the second one since it is destroyed if any of its elements disappear

                if (black.GetComponent<Wire>().element2 == black.GetComponent<Wire>().element1)//clicked on the same object
                {
                    Destroy(black.gameObject);
                    red = null;
                    blue = null;
                }
                else
                {
                    if (red == null)
                    {
                        red = black;
                    }
                    else
                    {
                        blue = black;
                    }
                }
                controller.GetComponent<Controller>().wireConnection = null;//clear the controller
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
