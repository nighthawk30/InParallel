using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHover : MonoBehaviour
{
    float tileScale;

    public bool HoverTest()
    {
        tileScale = 40 * Screen.width / 947;//scale edge detection with screen width - made for a 947 screen width
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