using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] Bullet bulletPrefab;

    //boost setup
    float boost = 0.15f;//accelerations
    float dragratio = 0.01f;//drag on ship - causes drift effect v1 - relative to ships velocity
    float radAngle;
    
    Vector3 speed = new Vector3(0, 0, 0);//total control
    Vector3 accel = new Vector3(0, 0, 0);
    Vector3 drag = new Vector3(0, 0, 0);

    float turn = 180;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //acceleration set
        radAngle = Mathf.Deg2Rad * (transform.eulerAngles.z + 90);
        accel.x = boost * Mathf.Cos(radAngle) * Time.deltaTime;
        accel.y = boost * Mathf.Sin(radAngle) * Time.deltaTime;
        //boost - add acceleration to speed
        if (Input.GetKey(KeyCode.W))
        {
            speed += accel;
        }
        //drag - subtract a proportional amount of speed from the objects speed
        drag.x = speed.x * dragratio;
        drag.y = speed.y * dragratio;
        speed -= drag;
        //move
        transform.position += speed;
        
        //turn left
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, 0, turn * Time.deltaTime);
        }
        //turn right
        if(Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 0, -turn * Time.deltaTime);
        }
        //Fire
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void Fire()
    {
        Bullet bolt = Instantiate(bulletPrefab, transform.position, transform.rotation);//Setting the position and bullet rotation to that of the ship upon init
    }
}
