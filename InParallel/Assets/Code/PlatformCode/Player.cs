using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //key
    //move probe
    //if no intersect move
    //if intersect move dist to intersect
    public GameObject pcontrol;
    float speed;
    float advance;
    float radAngle;
    float rotate = 0;
    Vector2 probe = new Vector2(0, 0);//position of probe test
    Vector2 direction = new Vector2(0, 0);//real movement unit vector
    Vector2 orientation = new Vector2(0, 0);//unity vector orientation of rotation
    Vector2 searchDimension = new Vector2(0, 0);
    RaycastHit2D hit;
    Vector3 jump = new Vector3(0, 0, 0);
    Vector3 velocity = new Vector3(0, 0, 0);
    int[] edge = new int[3];
    int[] prevedge = new int[3];
    bool metaledge;

    // Start is called before the first frame update
    void Start()
    {
        speed = 200;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))//in the future only do this if you have boots, they are powered, you are on metal
        {
            pcontrol.transform.Rotate(0, 0, 90);
        }
        advance = speed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, 0, pcontrol.transform.eulerAngles.z);//rotate
        radAngle = Mathf.Deg2Rad * transform.eulerAngles.z;
        searchDimension = new Vector2(GetComponent<BoxCollider2D>().size.x / 2, GetComponent<BoxCollider2D>().size.y / 2);
        orientation = new Vector2(Mathf.Cos(radAngle), Mathf.Sin(radAngle));
        Rotate();

        //set move
        velocity = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.A))
        {
            //account for float error and direction
            if (Mathf.Abs(Mathf.Cos(radAngle)) > 0.01f)
            {
                velocity.x += -advance * Mathf.Cos(radAngle);
            }
            if (Mathf.Abs(Mathf.Sin(Mathf.Deg2Rad * transform.eulerAngles.z)) > 0.01f)
            {
                velocity.y += -advance * Mathf.Sin(radAngle);
            }
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            if (Mathf.Abs(Mathf.Cos(radAngle)) > 0.01f)
            {
                velocity.x += advance * Mathf.Cos(radAngle);
            }
            if (Mathf.Abs(Mathf.Sin(radAngle)) > 0.01f)
            {
                velocity.y += advance * Mathf.Sin(radAngle);
            }
        }
        Move();
    }

    void Rotate()//should it rotate and in which direction
    {
        //spin if:
        /*
         * Touching corner of two metal walls or if at the edge of a metal platform
         * Boots are emag - later
         */
        MetalCorner();
        MetalEdge();
    }

    void MetalEdge()//on the edge of a metal block
    {
        RaycastHit2D[] adjacent = new RaycastHit2D[3];//DOWN: left center right
        //bottom left
        probe.x = transform.position.x + (orientation.y - orientation.x) * searchDimension.x + orientation.y;//(sin-cos)s + sin
        probe.y = transform.position.y + (-orientation.y - orientation.x) * searchDimension.y - orientation.x;//(-sin-cos)s - cos
        adjacent[0] = Physics2D.Linecast(probe, probe);
        //bottom center
        probe.x = transform.position.x + orientation.y * (searchDimension.x + 1);
        probe.y = transform.position.y - orientation.x * (searchDimension.y + 1);
        adjacent[1] = Physics2D.Linecast(probe, probe);
        //bottom right
        probe.x = transform.position.x + (orientation.y + orientation.x) * searchDimension.x + orientation.y;//(sin+cos)s + sin
        probe.y = transform.position.y + (orientation.y - orientation.x) * searchDimension.y - orientation.x;//(sin-cos)s - cos
        adjacent[2] = Physics2D.Linecast(probe, probe);
        
        int[] edge = new int[3];
        for (int i = 0; i < 3; i++)
        {
            edge[i] = 0;//airs
            if (adjacent[i].collider != null)
            {
                if (adjacent[i].collider.gameObject.GetComponent<Wall>().isMetal)
                {
                    edge[i] = 1;//metal
                }
                else
                {
                    edge[i] = -1;//normal
                }
            }
            //Debug.Log(i + " : " + edge[i]);
        }
        
        if (edge[0] == 1 && edge[1] == 0 && edge[2] == 0 && Input.GetKey(KeyCode.D))//edge to left
        {
            advance = Mathf.Abs(Mathf.Abs(adjacent[0].collider.gameObject.transform.position.x - transform.position.x)
                -Mathf.Abs(adjacent[0].collider.gameObject.transform.position.y - transform.position.y));
            pcontrol.transform.Rotate(0, 0, -90);
        }
        if (edge[0] == 0 && edge[1] == 0 && edge[2] == 1 && Input.GetKey(KeyCode.A))//edge to right
        {
            advance = Mathf.Abs(Mathf.Abs(adjacent[2].collider.gameObject.transform.position.x - transform.position.x)
                - Mathf.Abs(adjacent[2].collider.gameObject.transform.position.y - transform.position.y));
            pcontrol.transform.Rotate(0, 0, 90);
        }
        prevedge = edge;
    }

    void  MetalCorner()//handles corner room rotation
    {
        RaycastHit2D[] adjacent = new RaycastHit2D[3];//left down right
        probe = new Vector2(transform.position.x - orientation.x * (searchDimension.x + 1), transform.position.y - orientation.y * (searchDimension.y + 1));
        adjacent[0] = Physics2D.Linecast(probe, probe);
        probe = new Vector2(transform.position.x + orientation.y * (searchDimension.x + 1), transform.position.y - orientation.x * (searchDimension.y + 1));
        adjacent[1] = Physics2D.Linecast(probe, probe);
        probe = new Vector2(transform.position.x + orientation.x * (searchDimension.x + 1), transform.position.y + orientation.y * (searchDimension.y + 1));
        adjacent[2] = Physics2D.Linecast(probe, probe);

        bool[] corner = new bool[3];
        for (int i = 0; i < 3; i++)
        {
            if (adjacent[i].collider != null)
            {
                if (adjacent[i].collider.gameObject.GetComponent<Wall>().isMetal)
                {
                    corner[i] = true;
                }
            }
        }
        if (corner[0] && corner[1] && Input.GetKey(KeyCode.A))//left corner && moving left
        {
            pcontrol.transform.Rotate(0, 0, -90);
        }
        if (corner[1] && corner[2] && Input.GetKey(KeyCode.D))//right corner && moving right
        {
            pcontrol.transform.Rotate(0, 0, 90);
        }
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
