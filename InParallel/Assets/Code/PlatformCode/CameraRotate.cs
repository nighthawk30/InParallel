using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))//in the future only do this if you have boots, they are powered, you are on metal
        {
            transform.Rotate(0, 0, 90);
        }
    }
}
