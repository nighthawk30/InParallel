using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    //to create selector icons but might need 1 for each depending on how its done, hopefully can just change sprite and name
    public GameObject batteryIcon;
    public GameObject lightIcon;
    [SerializeField] GridSlot gridSlot;//to create grid tiles
    public GameObject wireToggle;
    public GameObject buildButton;

    public GameObject currentSelection = null;//what the user is selecting this a way of passing references between objects
    public bool wireSwitch = false;//is the wire toggle on
    public GameObject wireConnection;//the wire itself

    GridSlot grid;
    int gridSize = 25;
    public GameObject[] board = new GameObject[25];

    // Start is called before the first frame update
    void Start()//build ui
    {
        grid = Instantiate(gridSlot, transform.parent.GetChild(1));//output slot
        grid.gameObject.GetComponent<GridSlot>().controller = this.gameObject;//tell the tile that this is the controller

        //add all tiles to grid
        for (int i = 0; i < gridSize; i++)
        {
            //Instantiate(Object original, Transform parent);
            grid = Instantiate(gridSlot, transform.parent.GetChild(2));//slot it into the second child of the canvas, the grid, dont adjust scale
            grid.gameObject.GetComponent<GridSlot>().controller = this.gameObject;//tell the tile that this is the controller
            board[i] = grid.gameObject;
        }

        GameObject build = Instantiate(buildButton, transform.parent.GetChild(3));//slot it into the third child of the canvas, the tool panel, dont adjust scale
        build.gameObject.GetComponent<BuildButton>().controller = this.gameObject;

        //add selector tiles to toolbar - probably individually, they will just be children of the main one I think - diff sprite and name
        GameObject battery = Instantiate(batteryIcon, transform.parent.GetChild(4));//slot it into the third child of the canvas, the tool panel, dont adjust scale
        battery.gameObject.GetComponent<SelectorIcon>().controller = this.gameObject;//tell the selector that this is the controller
        GameObject light = Instantiate(lightIcon, transform.parent.GetChild(4));//slot it into the third child of the canvas, the tool panel, dont adjust scale
        light.gameObject.GetComponent<SelectorIcon>().controller = this.gameObject;

        //add wiretoggle to toggle bar
        GameObject toggle = Instantiate(wireToggle, transform.parent.GetChild(4));//slot it into the third child of the canvas, the tool panel, dont adjust scale
        toggle.gameObject.GetComponent<WireToggle>().controller = this.gameObject;
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
