using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wire : MonoBehaviour
{
    //Rotate and scale a wire image to connect the two components
    public GameObject element1;
    public GameObject element2;
    public GameObject controller;
    public GameObject otherWire;//if there is a double wire - 2 elements double connected - coordinate with it
    public Sprite hoveron;//the wire is hovered
    public Sprite hoveroff;//the wire is not hovered
    public Sprite select;//the wire is selected
    public float shiftDirection;//if there are double wires, the shift separates them
    int cutCount = 0;//state for wire cutting 0: nothing 1: hover 2: click1 3: click2 - cut
    bool set = false;//are both elements set to a tile
    float positionAdd;//for altering position in case of double wires
    Vector2 mouse = new Vector2(0, 0);//mouse position
    Vector2 point1 = new Vector2(0, 0);//location of element1 - drawing purposes
    Vector2 point2 = new Vector2(0, 0);//location of element2 - drawing purposes

    // Update is called once per frame
    void Update()
    {
        //System for hovering selecting and cutting a wire - cannot both place and cut wires
        HoverCut();
        //the other end has been placed
        if (element2 != null)
        {
            set = true;
        }
        //system for drawing wires and determining if they should be cut by default
        if (set)//the wire is set
        {
            if (element1 == null || element2 == null)//an element was removed
            {
                Destroy(gameObject);
            }
            else
            {
                DoubleWire();//Test and account for two wires connecting 2 components
                //draw between objects
                point1.x = element1.transform.position.x + positionAdd;
                point1.y = element1.transform.position.y;
                point2.x = element2.transform.position.x + positionAdd;
                point2.y = element2.transform.position.y;
                Draw(point1, point2);
            }
        }
        else//the wire is not set
        {
            if (Input.GetMouseButtonDown(0))//they have clicked and not set it to a tile
            {
                Destroy(gameObject);//if you click somewhere other than an empty tile
            }
            else
            {
                mouse.x = Input.mousePosition.x;
                mouse.y = Input.mousePosition.y;
                point1.x = element1.transform.position.x;
                point1.y = element1.transform.position.y;
                Draw(point1, mouse);
            }
        }
    }

    void HoverCut()
    {
        if (Input.GetMouseButtonDown(0) && controller.GetComponent<RaycastHover>().rayHover == gameObject &&
            !controller.GetComponent<Controller>().wireSwitch)//hover and click
        {
            cutCount++;
        }
        else if (controller.GetComponent<RaycastHover>().rayHover == gameObject && 
            !controller.GetComponent<Controller>().wireSwitch)//just hover
        {
            GetComponent<Image>().sprite = hoveron;
        }
        else if (Input.GetMouseButtonDown(0))//just click - not on the wire
        {
            cutCount = 0;
        }
        else//no action
        {
            GetComponent<Image>().sprite = hoveroff;
        }

        switch (cutCount)
        {
            case 1:
                GetComponent<Image>().sprite = select;//wire selected
                break;
            case 2:
                Destroy(gameObject);//wire cut
                break;
        }
    }

    void Draw(Vector2 From, Vector2 To)//draw line between two points
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(2 * Vector2.Distance(From, To) * 947f / Screen.width, GetComponent<RectTransform>().sizeDelta.y);//scale to distance and account for screen resize
        transform.eulerAngles = new Vector3(0, 0, -Mathf.Atan2((To.x - From.x), (To.y - From.y)) * Mathf.Rad2Deg + 90);//proper angle between the two points
        transform.position = (To - From) / 2 + From;//shift to halfway between
        GetComponent<BoxCollider2D>().size = new Vector2(2 * Vector2.Distance(From, To) * 947f / Screen.width, GetComponent<BoxCollider2D>().size.y);//scale collider as well
    }

    bool DoubleWire()//adds a shift to the wire position in case of a double wire, to separate them
    {
        //set otherwire to the other wire connected to element1
        if (element1.GetComponent<Tile>().red == this.gameObject)//the red wire of element1 is this object
        {
            otherWire = element1.GetComponent<Tile>().blue;//the blue wire of element1 must be the other wire attached - could be null
        }
        if (element1.GetComponent<Tile>().blue == this.gameObject)//the red wire of element1 is this object - prob dont need if, just else
        {
            otherWire = element1.GetComponent<Tile>().red;
        }
        
        if (otherWire != null)
        {
            //test if the two wires have identical elements - need to redo if want more than 2 wires per element
            if ((otherWire.GetComponent<Wire>().element1 == element1 && otherWire.GetComponent<Wire>().element2 == element2) ||
                (otherWire.GetComponent<Wire>().element2 == element1 && otherWire.GetComponent<Wire>().element1 == element2))
            {
                //this works because they each set their own and the other. Update order should mean that one overwrites the other - pretty inefficient
                shiftDirection += 1;//update order shenanigans to set wires with opposite offset
                otherWire.GetComponent<Wire>().shiftDirection -= 1;
                positionAdd = 40 * shiftDirection - 20;
                positionAdd *= Screen.width / 1553f;//scale to screen width;
                return true;
            }
        }
        positionAdd = 0;
        return false;
    }
}
