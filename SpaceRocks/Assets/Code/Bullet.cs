using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //bullet movement
    float speed = 20;
    float xspeed;
    float yspeed;
    float radAngle;
    //Bullet despawn
    float despawnDist = 800;
    float currentDist = 0;

    void Start()//private by default
    {
        radAngle = Mathf.Deg2Rad * (transform.eulerAngles.z + 90);
        xspeed = speed * Mathf.Cos(radAngle);
        yspeed = speed * Mathf.Sin(radAngle);
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(xspeed * Time.deltaTime, yspeed * Time.deltaTime, 0);//add x and y component of speed to position in its direction
        currentDist += speed;//add the distance to the total distance traveled
        Despawn();//trigger despawn attempt
    }

    void Despawn()//kill bullet after certain distance
    {
        if (currentDist >= despawnDist)
        {
            Destroy(gameObject);
        }
    }
}