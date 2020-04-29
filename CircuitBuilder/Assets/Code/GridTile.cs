using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridTile : MonoBehaviour
{
    public Sprite BuildTile;
    public Sprite HoverTile;
    bool place;//has something been placed on this tile
    public GameObject controller;
    bool hover;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        hover = GetComponent<TileHover>().HoverTest();
        if (!place)//if this tile has not been filled
        {
            if (hover)//test to switch the sprite
            {
                GetComponent<Image>().sprite = HoverTile;
            }
            else
            {
                GetComponent<Image>().sprite = BuildTile;
            }
        }
        TilePlace();//test to place a tile on this grid square
    }

    void TilePlace()
    {
        if (hover && Input.GetMouseButtonUp(0) && controller.GetComponent<Controller>().currentSelection != null)//if you drop an icon on this tile
        {
            GetComponent<Image>().sprite = controller.GetComponent<Controller>().currentSelection.GetComponent<Image>().sprite;//switch this sprite to that of the one that was dropped
            //in the future, also change properties or name of the tile
            //we also want to be able to pick up this tile and move it
            place = true;//a tile has been placed on this grid square
        }
    }
}