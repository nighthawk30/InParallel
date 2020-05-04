using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSlot : MonoBehaviour
{
    public Sprite BuildTile;
    public Sprite HoverTile;
    public GameObject controller;
    GameObject newTile;//the tile object that this object becomes
    bool place;//has something been placed on this tile
    bool hover;//is the tile being hovered over

    // Update is called once per frame
    void Update()
    {
        hover = GetComponent<TileHover>().HoverTest();
        if (!place)//if this tile has not been filled
        {
            if (hover)//test to switch the sprite - should only add a highlight ring to the sprite
            {
                GetComponent<Image>().sprite = HoverTile;
                if (Input.GetMouseButtonUp(0) && controller.GetComponent<Controller>().currentSelection != null)//you dropped a component onto it
                {
                    TilePlace();
                    GetComponent<Image>().sprite = BuildTile;//switch back to basic tile as background
                }
            }
            else
            {
                GetComponent<Image>().sprite = BuildTile;
            }
        }
        else //turns it into a modified selector tile to work as an inventory slot
        {
            if (hover && Input.GetMouseButtonDown(0) && !controller.GetComponent<Controller>().wireSwitch)//checks that you arent trying to place a wire instead of move the object
            {
                TileRemove();//let the tile be moved
            }
        }
    }

    void TilePlace()
    {

        newTile = controller.GetComponent<Controller>().currentSelection;//create a reference to the tile that is on it
        newTile.GetComponent<Tile>().gridTile = this.gameObject;//tell the tile that has been dropped on it that this is where it should be
        place = true;//a tile has been placed on this grid square
    }

    void TileRemove()//what to do if the user clicks the tile in the grid - inventory
    {
        place = false;//the tile is no longer on the grid square, so it is free to move
        newTile.GetComponent<Tile>().gridTile = null;//set the tile free
        newTile = null;//forget the tile
    }
}