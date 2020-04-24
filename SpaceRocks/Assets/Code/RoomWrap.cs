using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomWrap : MonoBehaviour
{
    float wrapx = 11;
    float wrapy = 5;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -wrapx)
        {
            transform.position += new Vector3(2 * wrapx, 0, 0);
        }
        else if (transform.position.x > wrapx)
        {
            transform.position += new Vector3(-2 * wrapx, 0, 0);
        }
        if (transform.position.y > wrapy)
        {
            transform.position += new Vector3(0, -2 * wrapy, 0);
        }
        else if (transform.position.y < -wrapy)
        {
            transform.position += new Vector3(0, 2 * wrapy, 0);
        }
    }
}
