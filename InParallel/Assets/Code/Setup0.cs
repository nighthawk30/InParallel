using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setup0 : MonoBehaviour
{
    //setup variables
    [SerializeField] GridSlot gridSlot;//to create grid tiles
    public GameObject batteryIcon;
    public GameObject zincIcon;
    public GameObject copperIcon;
    public GameObject lightIcon;
    public GameObject wireToggle;
    public GameObject buildButton;
    public GameObject craftTile;

    GridSlot grid;
    int gridSize = 25;
    public GameObject[] board;

    // Start is called before the first frame update
    void Start()//build ui
    {
        board = new GameObject[gridSize];
        //add all tiles to grid
        for (int i = 0; i < gridSize; i++)
        {
            //Instantiate(Object original, Transform parent);
            grid = Instantiate(gridSlot, transform.parent.GetChild(1).GetChild(0));//slot it into the second child of the canvas, the grid, dont adjust scale
            grid.gameObject.GetComponent<GridSlot>().controller = this.gameObject;//tell the tile that this is the controller
            board[i] = grid.gameObject;
        }
        //Add Selector Tiles
        GameObject battery = Instantiate(batteryIcon, transform.parent.GetChild(1).GetChild(1));//slot it into the third child of the canvas, the tool panel, dont adjust scale
        battery.gameObject.GetComponent<SelectorIcon>().controller = this.gameObject;//tell the selector that this is the controller
        GameObject light = Instantiate(lightIcon, transform.parent.GetChild(1).GetChild(1));//slot it into the third child of the canvas, the tool panel, dont adjust scale
        light.gameObject.GetComponent<SelectorIcon>().controller = this.gameObject;
        GameObject toggle = Instantiate(wireToggle, transform.parent.GetChild(1).GetChild(1));//slot it into the third child of the canvas, the tool panel, dont adjust scale
        toggle.gameObject.GetComponent<WireToggle>().controller = this.gameObject;
        //Add Craft Window
        GameObject craft = Instantiate(craftTile, transform.parent.GetChild(1).GetChild(2));//output slot
        craft.gameObject.GetComponent<CraftTile>().controller = this.gameObject;//tell the tile that this is the controller

        GetComponent<Controller>().board = board;//continuity
    }
}
