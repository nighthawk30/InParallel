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
    public GameObject canvas;
    bool set = false;
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
                if (Input.GetMouseButtonDown(0))
                {
                    Destroy(gameObject);//if you click somewhere other than an empty tile
                }
            }
            else
            {
                //draw between objects
                point1.x = element1.transform.position.x;
                point1.y = element1.transform.position.y;
                point2.x = element2.transform.position.x;
                point2.y = element2.transform.position.y;
                Draw(point1, point2);

            }
        }
    }

    void Draw(Vector2 From, Vector2 To)
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(2 * Vector2.Distance(From, To) * 947 / Screen.width, GetComponent<RectTransform>().sizeDelta.y);//scale to distance and account for screen resize
        transform.eulerAngles = new Vector3(0, 0, -Mathf.Atan2((To.x - From.x), (To.y - From.y)) * Mathf.Rad2Deg + 90);//rotate to angle
        transform.position = (To - From) / 2 + From;//shift to halfway between
    }
}
