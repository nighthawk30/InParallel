using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    //to create selector icons but might need 1 for each depending on how its done, hopefully can just change sprite and name
    public GameObject batteryIcon;
    public GameObject lightIcon;
    [SerializeField] GridSlot gSlot;//to create grid tiles
    
    public GameObject currentSelection = null;//what the user is selecting
    public bool wireToggle = false;//is the wire toggle on
    public GameObject wireConnection;//the wire itself

    GridSlot grid;
    int gridSize = 25;

    /*
     * the only way to break a wire is to delete the wire or one of the connector objects
     * wire moves with objects it is bound to
     * when created first, the wire has start 
     * turn off wireToggle by clicking the toggle or another selector
     */

    /*
     * The objects never disappear they only snap to the grid
     * The are over the grid
     */

    //The idea is that if the controller places in all the tiles, it can monitor which ones are being selected and tell the grid which are being placed
    //This will also be useful for circuit testing

    // Start is called before the first frame update
    void Start()//build ui
    {
        //add all tiles to grid
        for (int i = 0; i < gridSize; i++)
        {
            //Instantiate(Object original, Transform parent);
            grid = Instantiate(gSlot, transform.parent.GetChild(1));//slot it into the second child of the canvas, the grid, dont adjust scale
            grid.gameObject.GetComponent<GridSlot>().controller = this.gameObject;//tell the tile that this is the controller
        }

        //add selector tiles to toolbar - probably individually, they will just be children of the main one I think - diff sprite and name
        GameObject battery = Instantiate(batteryIcon, transform.parent.GetChild(2));//slot it into the third child of the canvas, the tool panel, dont adjust scale
        battery.gameObject.GetComponent<SelectorIcon>().controller = this.gameObject;//tell the selector that this is the controller
        GameObject light = Instantiate(lightIcon, transform.parent.GetChild(2));//slot it into the third child of the canvas, the tool panel, dont adjust scale
        light.gameObject.GetComponent<SelectorIcon>().controller = this.gameObject;

        //add wiretoggle to toggle bar
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
    }

    public void Quit()
    {
        Application.Quit();//for builds
        //UnityEditor.EditorApplication.isPlaying = false;//for editor
    }
}
