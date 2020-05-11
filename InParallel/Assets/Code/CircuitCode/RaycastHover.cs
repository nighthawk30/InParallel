using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastHover : MonoBehaviour
{
    RaycastHit2D hit;
    public GameObject rayHover;//the object that is being hovered over

    void Update()
    {
        rayHover = RayHover();
    }

    public GameObject RayHover()
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
