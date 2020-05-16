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
    public GameObject craftTile;
    public GameObject toolTip;

    public GameObject currentSelection = null;//what the user is selecting this a way of passing references between objects
    public bool wireSwitch = false;//is the wire toggle on
    public GameObject wireConnection;//the wire itself

    GridSlot grid;
    int gridSize = 25;
    public GameObject[] board = new GameObject[25];
    bool corunning = false;

    // Start is called before the first frame update
    void Start()//build ui
    {
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
        //Add tooltip ui
        toolTip = Instantiate(toolTip, transform.parent.GetChild(4));//Draw layer +4 - topmost
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
        //testing
        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(DelayBuild(0.1f));
        }
    }

    IEnumerator DelayBuild(float time)
    {
        if (corunning)
        {
            yield break;
        }
        corunning = true;
        yield return new WaitForSeconds(time);
        //
        GetComponent<BuildCircuit>().Build();
        //
        corunning = false;
    }

    public void Quit()
    {
        Application.Quit();//for builds
        //UnityEditor.EditorApplication.isPlaying = false;//for editor
    }
}
