using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public GameObject camera;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        GameObject cam = Instantiate(camera);
        cam.GetComponent<CameraRotate>().pcontrol = this.gameObject;
        GameObject p1 = Instantiate(player);
        p1.GetComponent<Player>().pcontrol = this.gameObject;
        p1.transform.position = new Vector3(-32, -128, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
