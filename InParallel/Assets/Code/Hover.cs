using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//is the cursor over the object
public class Hover : MonoBehaviour
{
    //returns if the cursor is in an arbitrary box
    float tileScale;
    public bool HoverTest()
    {
        tileScale = 39 * Screen.width / 947f;//scale edge detection with screen width - made for a 947 screen width
        if (Mathf.Abs(Input.mousePosition.x - transform.position.x) < tileScale && Mathf.Abs(Input.mousePosition.y - transform.position.y) < tileScale)//scale edge based on screen size?
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
