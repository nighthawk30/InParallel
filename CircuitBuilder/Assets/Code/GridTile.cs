using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridTile : MonoBehaviour
{
    public Sprite BuildTile;
    public Sprite HoverTile;
    bool colliding;
    bool place;
    GameObject tile;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!place)
        {
            if (GetComponent<TileHover>().HoverTest())
            {
                GetComponent<Image>().sprite = HoverTile;
                if (Input.GetMouseButtonUp(0) && colliding)
                {
                    GetComponent<Image>().sprite = tile.GetComponent<Image>().sprite;
                    place = true;
                }
            }
            else
            {
                GetComponent<Image>().sprite = BuildTile;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        colliding = true;
        tile = other.gameObject;
        Debug.Log("true");
    }

    void OnTriggerExit(Collider other)
    {
        colliding = false;
    }

}