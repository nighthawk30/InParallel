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
    public GameObject otherWire;
    public float shiftDirection;
    bool set = false;
    float positionAdd;//for altering position in case of double wires
    Vector2 mouse = new Vector2(0, 0);
    Vector2 point1 = new Vector2(0, 0);
    Vector2 point2 = new Vector2(0, 0);

    // Update is called once per frame
    void Update()
    {
        if (set && (element1 == null || element2 == null))//if the wire was placed and then an element was destroyed, break it
        {
            Destroy(gameObject);
        }
        else
        {
            if (element2 != null)//the other end has been placed
            {
                set = true;
            }

            if (!set)
            {
                //draw between obj1 and mouse
                //set position
                mouse.x = Input.mousePosition.x;
                mouse.y = Input.mousePosition.y;
                point1.x = element1.transform.position.x;
                point1.y = element1.transform.position.y;
                Draw(point1, mouse);
                if (Input.GetMouseButtonDown(0))//update order shenanigans - will always destroy the wire when you click, but if you click on a tile, this code wont run bc it will then be set
                {
                    Destroy(gameObject);//if you click somewhere other than an empty tile
                }
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
    }

    void Draw(Vector2 From, Vector2 To)
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(2 * Vector2.Distance(From, To) * 947f / Screen.width, GetComponent<RectTransform>().sizeDelta.y);//scale to distance and account for screen resize
        transform.eulerAngles = new Vector3(0, 0, -Mathf.Atan2((To.x - From.x), (To.y - From.y)) * Mathf.Rad2Deg + 90);//proper angle between the two points
        transform.position = (To - From) / 2 + From;//shift to halfway between
    }

    bool DoubleWire()//adds a shift to the wire position in case of a double wire, to separate them - not very efficient
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
