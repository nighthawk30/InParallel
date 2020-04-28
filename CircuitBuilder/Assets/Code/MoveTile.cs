using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTile : MonoBehaviour
{
    Vector3 cursorLocation = new Vector3(0, 0, 0);
    float screenRatio;
    // Start is called before the first frame update
    void Start()
    {
        screenRatio = 160 / ((float)(Screen.height));
    }

    // Update is called once per frame
    void Update()
    {
        cursorLocation.x = (Input.mousePosition.x - Screen.width / 2) * screenRatio;
        cursorLocation.y = (Input.mousePosition.y - Screen.height / 2) * screenRatio;
        Debug.Log(cursorLocation.x);
        transform.position = cursorLocation;
    }
}
