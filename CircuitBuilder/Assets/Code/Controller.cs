using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] SelectorTile sTile;//to create selector icons but might need 1 for each depending on how its done, hopefully can just change sprite and name
    [SerializeField] GridTile gTile;//to create grid tiles
    
    public GameObject currentSelection = null;//what the user is selecting
    GridTile grid;
    SelectorTile square;

    //The idea is that if the controller places in all the tiles, it can monitor which ones are being selected and tell the grid which are being placed
    //This will also be useful for circuit testing

    // Start is called before the first frame update
    void Start()//build ui
    {
        //add all tiles to grid
        for (int i = 0; i < 25; i++)
        {
            //Instantiate(Object original, Transform parent);
            grid = Instantiate(gTile, transform.parent.GetChild(1));//slot it into the second child of the canvas, the grid, dont adjust scale
            grid.gameObject.GetComponent<GridTile>().controller = this.gameObject;//tell the tile that this is the controller
        }

        //add selector tiles to toolbar - probably individually, they will just be children of the main one I think - diff sprite and name
        square = Instantiate(sTile, transform.parent.GetChild(2));//slot it into the third child of the canvas, the tool panel, dont adjust scale
        square.gameObject.GetComponent<SelectorTile>().controller = this.gameObject;//tell the selector that this is the controller
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
