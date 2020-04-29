using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridTile : MonoBehaviour
{
    public Sprite BuildTile;
    public Sprite HoverTile;
    public GameObject controller;
    GameObject newTile;//the tile object that this object becomes
    public DragTile dragTile;//this is purely for switch capabilities - inventory
    bool place;//has something been placed on this tile
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
            if (hover)//test to switch the sprite - should only add a highlight ring to the sprite
            {
                GetComponent<Image>().sprite = HoverTile;
                TilePlace();//test to place a tile on this grid square - overwrite should be not possible, inventory
            }
            else
            {
                GetComponent<Image>().sprite = BuildTile;
            }
        }
        else //turns it into a modified selector tile to work as an inventory slot
        {
            GridSelect();//let the tile be moved
        }
    }

    void TilePlace()
    {
        if (hover && Input.GetMouseButtonUp(0) && controller.GetComponent<Controller>().currentSelection != null)//if you drop an icon on this tile
        {
            newTile = controller.GetComponent<Controller>().currentSelection;
            GetComponent<Image>().sprite = newTile.GetComponent<Image>().sprite;//switch this sprite to that of the one that was dropped
            //in the future, also change properties or name of the tile
            //we also want to be able to pick up this tile and move it
            place = true;//a tile has been placed on this grid square
        }
    }

    void GridSelect()//what to do if the user clicks the tile in the grid - inventory
    {
        if (Input.GetMouseButtonDown(0) && hover)
        {
            DragTile vroom = Instantiate(dragTile, transform.position, transform.rotation, transform.parent.transform.parent);//Instantiate(Object original, Vector3 position, Quaternion rotation, Transform parent);
            vroom.GetComponent<DragTile>().controller = controller;//give the tile a reference to the controller
            controller.GetComponent<Controller>().currentSelection = newTile;//set the object that the controller creates - rn just copies sprite
            place = false;//the tile is no longer on the grid square, so it is free to move
            newTile = null;//just to be consistent
        }
    }
}