using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastHover : MonoBehaviour
{
    //The (1) topmost object you are hovering over - depends on collider rather than arbitrary bounds - both hover tests are necessary
    RaycastHit2D hit;
    public GameObject rayHover;//the object that is being hovered over

    void Update()
    {
        rayHover = RayHover();
    }

    public GameObject RayHover()//returns the first object of the collider that the raycast hit
    {
        hit = Physics2D.Linecast(Input.mousePosition, Input.mousePosition);
        if (hit.collider != null)
        {
            return hit.collider.gameObject;
        }
        else
        {
            return null;
        }
    }
}
