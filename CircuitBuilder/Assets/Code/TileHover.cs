using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHover : MonoBehaviour
{
    
    float screenRatio = 160 / ((float)(Screen.height));
    Vector2 cursorDistance = new Vector2(0, 0);

    /*public bool hovering;
    void Update()
    {
        cursorDistance.x = Mathf.Abs((Input.mousePosition.x - Screen.width / 2) * screenRatio - transform.position.x);
        cursorDistance.y = Mathf.Abs((Input.mousePosition.y - Screen.height / 2) * screenRatio - transform.position.y);
        if (cursorDistance.x < 32 && cursorDistance.y < 32)//32 bc the object is scaled by 2
        {
            hovering = true;
        }
        else
        {
            hovering = false;
        }
    }
    */

    public bool HoverTest()
    {
        cursorDistance.x = Mathf.Abs((Input.mousePosition.x - Screen.width / 2) * screenRatio - transform.position.x);
        cursorDistance.y = Mathf.Abs((Input.mousePosition.y - Screen.height / 2) * screenRatio - transform.position.y);
        if (cursorDistance.x < 16 && cursorDistance.y < 16)//32 bc the object is scaled by 2
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}