using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://gamedev.stackexchange.com/questions/167317/scale-camera-to-fit-screen-size-unity
public class ScaleCamera : MonoBehaviour
{
    public float sceneWidth = 30;
    float unitsPerPixel;
    float desiredHalfHeight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        unitsPerPixel = sceneWidth / Screen.width;
        desiredHalfHeight = 0.5f * unitsPerPixel * Screen.height;
        GetComponent<Camera>().orthographicSize = desiredHalfHeight;
    }
}
