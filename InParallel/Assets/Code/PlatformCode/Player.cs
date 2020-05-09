using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //key
    //move probe
    //if no intersect move
    //if intersect move dist to intersect
    float speed;
    Vector2 probe = new Vector2(0, 0);
    Vector2 direction = new Vector2(0, 0);
    RaycastHit2D hit;
    Vector3 jump = new Vector3(0, 0, 0);
    Vector3 velocity = new Vector3(0, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        speed = 200;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))//rotate the player with the camera so that the directions are the same
        {
            transform.Rotate(0, 0, 90);
        }

        velocity = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.A))
        {
            //account for float error and direction
            if (Mathf.Abs(Mathf.Cos(Mathf.Deg2Rad * transform.eulerAngles.z)) > 0.01f)
            {
                velocity.x = -speed * Mathf.Cos(Mathf.Deg2Rad * transform.eulerAngles.z) * Time.deltaTime;
            }
            if (Mathf.Abs(Mathf.Sin(Mathf.Deg2Rad * transform.eulerAngles.z)) > 0.01f)
            {
                velocity.y = -speed * Mathf.Sin(Mathf.Deg2Rad * transform.eulerAngles.z) * Time.deltaTime;
            }
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            if (Mathf.Abs(Mathf.Cos(Mathf.Deg2Rad * transform.eulerAngles.z)) > 0.01f)
            {
                velocity.x = speed * Mathf.Cos(Mathf.Deg2Rad * transform.eulerAngles.z) * Time.deltaTime;
            }
            if (Mathf.Abs(Mathf.Sin(Mathf.Deg2Rad * transform.eulerAngles.z)) > 0.01f)
            {
                velocity.y = speed * Mathf.Sin(Mathf.Deg2Rad * transform.eulerAngles.z) * Time.deltaTime;
            }
        }

        Move();
    }

    void Move()//handles movement and collision
    {
        //set the direction of the probe vector in accord with the speed
        if (velocity.x != 0)
        {
            direction.x = Mathf.Sign(velocity.x);
        }
        else
        {
            direction.x = 0;
        }
        if (velocity.y != 0)
        {
            direction.y = Mathf.Sign(velocity.y);
        }
        else
        {
            direction.y = 0;
        }
        //set the probe position based on the direction of movement
        probe.x = transform.position.x + direction.x * GetComponent<BoxCollider2D>().size.x / 2;//-1: left 0: center 1: right
        probe.y = transform.position.y + direction.y * GetComponent<BoxCollider2D>().size.y / 2;//-1: down 0: center 1: up
        probe += (Vector2)velocity;//add speed to probe to see where it would land if you traveled there
        hit = Physics2D.Linecast(probe, probe);
        //Debug.Log("X:" + direction.x + " Y: " + direction.y);
        //test if you would collide if you moved
        if (hit.collider == null)
        {
            transform.position += velocity;
        }
        else//if you would collide, jump the remaining distance
        {
            
            jump.x = direction.x * (Mathf.Abs(hit.collider.gameObject.transform.position.x - transform.position.x) - GetComponent<BoxCollider2D>().size.x);
            jump.y = direction.y * (Mathf.Abs(hit.collider.gameObject.transform.position.y - transform.position.y) - GetComponent<BoxCollider2D>().size.y);
            transform.position += jump;
        }
    }
}
