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
    public GameObject circuitBattery;

    GridSlot grid;
    int gridSize = 25;
    GridSlot[] board = new GridSlot[25];
    List<GameObject> myCircuit = new List<GameObject>();//might be multiple in the future for complex circuits

    /*
     * the only way to break a wire is to delete the wire or one of the connector objects
     * wire moves with objects it is bound to
     * when created first, the wire has start 
     * In the future have wires automatically recolor based on connections?
     * Have more than two wires possible per element - need to check for double connections
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
            grid = Instantiate(gridSlot, transform.parent.GetChild(1));//slot it into the second child of the canvas, the grid, dont adjust scale
            grid.gameObject.GetComponent<GridSlot>().controller = this.gameObject;//tell the tile that this is the controller
            board[i] = grid;
        }

        //add selector tiles to toolbar - probably individually, they will just be children of the main one I think - diff sprite and name
        GameObject battery = Instantiate(batteryIcon, transform.parent.GetChild(2));//slot it into the third child of the canvas, the tool panel, dont adjust scale
        battery.gameObject.GetComponent<SelectorIcon>().controller = this.gameObject;//tell the selector that this is the controller
        GameObject light = Instantiate(lightIcon, transform.parent.GetChild(2));//slot it into the third child of the canvas, the tool panel, dont adjust scale
        light.gameObject.GetComponent<SelectorIcon>().controller = this.gameObject;

        //add wiretoggle to toggle bar
        GameObject toggle = Instantiate(wireToggle, transform.parent.GetChild(2));//slot it into the third child of the canvas, the tool panel, dont adjust scale
        toggle.gameObject.GetComponent<WireToggle>().controller = this.gameObject;

        GameObject build = Instantiate(buildButton, transform.parent.GetChild(3));//slot it into the third child of the canvas, the tool panel, dont adjust scale
        build.gameObject.GetComponent<BuildButton>().controller = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
    }

    public void BuildCircuit()//called by the build button to build the circuit
    {
        //search for battery - does not give a shit about tiles, only looks at component and wire flow
        for (int i = 0; i < board.Length; i++)//goes through the tiles and find the batteries
        {
            if (board[i].GetComponent<GridSlot>().newTile != null)//there is a tile on that grid slot
            {
                if (board[i].GetComponent<GridSlot>().newTile.GetComponent<Tile>().tileType == 1)//it is a battery
                {
                    circuitBattery = board[i].GetComponent<GridSlot>().newTile;
                }
            }
        }

        if (circuitBattery != null)
        {
            myCircuit.Clear();
            LoopCheck(circuitBattery, circuitBattery, null);//sets the element sequence in the loop connected to the first element - no elements mean no loop - also false means no loop
            CircuitType();
        }
        else
        {
            //there is no battery error
            Debug.Log("Build Error: There is no battery in this circuit. Without a voltage source, your circuits won't work");
        }
    }

    private void CircuitType()
    {
        if (myCircuit.Count >= 2)
        {
            int batteryCount = 0;
            int lightCount = 0;
            foreach (GameObject g in myCircuit)
            {
                if (g.GetComponent<Tile>().tileType == 2)//it is a light tile
                {
                    lightCount++;
                }
                else if (g.GetComponent<Tile>().tileType == 1)
                {
                    batteryCount++;
                }
            }

            if (batteryCount == myCircuit.Count)//error there are only batteries
            {
                Debug.Log("Build Error: Without any components to provide resistance, you risk serious damage to your power source");
            }
        }
        else//unconnected components
        {
            Debug.Log("Build Error: There are unconnected components");
        }
    }

    bool LoopCheck(GameObject start, GameObject current, GameObject wire)//recursively checks if you have reached the initial position and records element position
    {
        if (current.GetComponent<Tile>().red != null && current.GetComponent<Tile>().blue != null)//there are more wires - no loose ends - main run requirement
        {
            myCircuit.Add(current);//add the element on to the list, not sure if I should keep track of wires
            //step to next wire that isnt the one you just went down
            if (current.GetComponent<Tile>().red != wire)//you arent traveling the same wire back and forth
            {
                wire = current.GetComponent<Tile>().red;
            }
            else if (current.GetComponent<Tile>().blue != wire)
            {
                wire = current.GetComponent<Tile>().blue;
            }

            //step to the next element that isnt the element you just looked at
            if (wire.GetComponent<Wire>().element1 == current)
            {
                current = wire.GetComponent<Wire>().element2;
            }
            else if (wire.GetComponent<Wire>().element2 == current)
            {
                current = wire.GetComponent<Wire>().element1;
            }

            if (current == start)//stops the loop - if it wasnt a loop, there would be a loose end at some point
            {
                return true;
            }
            return LoopCheck(start, current, wire);
        }
        return false;
    }

    public void Quit()
    {
        Application.Quit();//for builds
        //UnityEditor.EditorApplication.isPlaying = false;//for editor
    }
}
