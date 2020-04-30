using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHover : MonoBehaviour
{
    public bool HoverTest()
    {
        if (Mathf.Abs(Input.mousePosition.x - transform.position.x) < 32 && Mathf.Abs(Input.mousePosition.y - transform.position.y) < 32)//scale edge based on screen size?
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}