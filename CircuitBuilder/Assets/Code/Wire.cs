using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject element1;
    public GameObject element2;
    public GameObject controller;
    LineRenderer line;
    bool set = false;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
        line.startWidth = 1;
        line.endWidth = 1;
        line.SetPosition(0, new Vector3(element1.transform.position.x, element1.transform.position.y, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (!set)
        {
            line.SetPosition(1, new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));//drag with mouse;
            if (element2 != null)
            {
                line.SetPosition(1, new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));//drag with mouse;
            }
        }
        else
        {
            if (element1 == null || element2 == null)
            {
                Destroy(gameObject);
            }
        }
    }
}
