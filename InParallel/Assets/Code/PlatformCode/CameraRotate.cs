using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pcontrol;
    GameObject player;
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0, 0, pcontrol.transform.eulerAngles.z);
    }
}
