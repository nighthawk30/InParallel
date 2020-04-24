using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] Rock rockPrefab;

    //movement;
    public float size;
    float speed;
    float radAngle;
    float xspeed;
    float yspeed;
    float spin;
    public GameObject controller;

    void Start()
    {
        transform.localScale = new Vector3(size / 3, size / 3, size / 3);
        speed = Random.Range(3, 6) / size;//set speed as a random range then divide it by size to get slower big rocks and faster small rocks
        radAngle = Random.Range(0, 360);
        xspeed = speed * Mathf.Cos(radAngle);//set the initial velocity of the rock
        yspeed = speed * Mathf.Sin(radAngle);
        spin = 3 * Random.Range(-10, 10);//set how fast the rock is spinning
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(xspeed * Time.deltaTime, yspeed * Time.deltaTime, 0);//add x and y component of speed to position in its direction
        transform.Rotate(0, 0, spin * Time.deltaTime);
        if (controller.GetComponent<Controller>().rockDestroy)
        {
            Destroy(gameObject);
        }
        //collision detection between bullet and ship with rocks nested for loops for boundary detection is more optimizable
    }

    //Collision trigger
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Bullet>() != null || other.gameObject.GetComponent<Ship>() != null)//tests if there is a bullet script on the object it collides with
        {
            Destroy(other.gameObject);//destroys the bullet
            Explode();
        }
        //dont do anything if two rocks collide
    }

    void Explode()//rock hits ship or bullet and breaks up
    {
        SplitRock(size - 1);
        SplitRock(size - 1);
        controller.GetComponent<Controller>().rockDestroyed();//private by default
        controller.GetComponent<Controller>().scoreChange();
        //spawn smaller rocks? only the smallest rocks should trigger the wave function
        Destroy(gameObject);
    }

    void SplitRock(float newSize)//creates a smaller version of the current rock
    {
        if (newSize > 0)
        {
            controller.GetComponent<Controller>().rocksInPlay++;//tell the controller you exist
            Rock stone = Instantiate(rockPrefab);
            stone.size = newSize;
            stone.transform.position = transform.position;
            stone.controller = controller;
        }
    }
}
