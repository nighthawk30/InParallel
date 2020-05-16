using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildCircuit : MonoBehaviour
{
    List<GameObject> batteryList = new List<GameObject>();//list of batteries in the grid
    List<List<GameObject>> circuitList = new List<List<GameObject>>();//a list of the circuits in the grid
    bool[] errorType = new bool[4];//keeps track of the errors logged in this build
    GameObject[] board;
    [SerializeField] Text boardText;

    public void Build()
    {
        board = GetComponent<Controller>().board;
        batteryList.Clear();
        circuitList.Clear();
        //search for battery - does not give a shit about tiles, only looks at component and wire flow
        for (int i = 0; i < board.Length; i++)//goes through the tiles and find the batteries
        {
            if (board[i].GetComponent<GridSlot>().newTile != null)//there is a tile on that grid slot
            {
                //a component is not fully connected
                if (board[i].GetComponent<GridSlot>().newTile.GetComponent<Tile>().red == null || 
                    board[i].GetComponent<GridSlot>().newTile.GetComponent<Tile>().blue == null)
                {
                    errorType[1] = true;
                }
                if (board[i].GetComponent<GridSlot>().newTile.GetComponent<Tile>().tileType == 1)//it is a battery
                {
                    batteryList.Add(board[i].GetComponent<GridSlot>().newTile);
                }
            }
        }

        //Add all of the unique circuit loops
        if (batteryList.Count > 0)
        {
            //object.Equals();
            foreach (GameObject battery in batteryList)
            {
                List<GameObject> circuit = LoopCheck(battery, battery, null, new List<GameObject>());//sets the element sequence in the loop connected to the first element - no elements mean no loop - also false means no loop
                if (circuit.Count > 1)
                {
                    bool loopRepeat = false;
                    foreach (List<GameObject> l in circuitList)//makes sure it isnt adding a duplicate list
                    {
                        if (loopCompare(l, circuit))//compare the two list
                        {
                            loopRepeat = true;
                            break;
                        }
                    }
                    if (!loopRepeat)//if not a duplicate, add it
                    {
                        circuitList.Add(circuit);
                    }
                }
                else//a circuit is empty -> unconnected components
                {
                    //errorType[1] = true;//0: no battery, 1: unconnected battery/components, 2: only batteries, 3: no resistors? - required
                }
            }
        }
        else//there is no battery
        {
            errorType[0] = true;//0: no battery, 1: unconnected battery/components, 2: only batteries, 3: no resistors? - redundant, only catches battery
        }

        //We have all of the circuits
        foreach (List<GameObject> circuit in circuitList)
        {
            CircuitType(circuit);
        }
        ErrorLog();
    }

    private bool loopCompare(List<GameObject> loop1, List<GameObject> loop2)//tests if the two lists are the same, in which case, dont make a new one - important for circuits with 2+ batteries
    {
        List<GameObject>.Enumerator e = loop2.GetEnumerator();//chooses the first object in the list because since each can only have 2 wires, there cant be repeats
        e.MoveNext();//it starts before the list so you need to move before getting current
        GameObject g2 = e.Current;
        foreach (GameObject g1 in loop1)
        {
            if (g1 == g2)
            {
                return true;
            }
        }
        return false;
    }

    private List<GameObject> LoopCheck(GameObject start, GameObject current, GameObject wire, List<GameObject> loop)//creates a list of the connected circuit components
    {
        if (current.GetComponent<Tile>().red != null && current.GetComponent<Tile>().blue != null)//there are more wires - no loose ends - main run requirement
        {
            loop.Add(current);//add the element on to the list, not sure if I should keep track of wires

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

            //stops the loop - if it wasnt a loop, there would be a loose end at some point
            if (current == start)
            {
                return loop;
            }
            return LoopCheck(start, current, wire, loop);
        }
        return loop;
    }

    private void CircuitType(List<GameObject> cType)//finds the kinds of elements in the circuit and produces appropriate errors
    {
        int batteryCount = 0;
        int lightCount = 0;
        int resistorCount = 0;//add later

        foreach (GameObject g in cType)
        {
            if (g.GetComponent<Tile>().tileType == 1)//it is a battery tile
            {
                batteryCount++;
            }
            else if (g.GetComponent<Tile>().tileType == 2)
            {
                lightCount++;
            }
            else if (g.GetComponent<Tile>().tileType == 3)
            {
                resistorCount++;
            }
        }

        if (batteryCount == cType.Count)//error there are only batteries
        {
            errorType[2] = true;//0: no battery, 1: unconnected battery/components, 2: only batteries, 3: no resistors? - required
        }
        /*
        if (resistorCount == 0)//error there are no resistors
        {
            errorType[3] = true;//0: no battery, 1: unconnected battery/components, 2: only batteries, 3: no resistors? - required
        }
         */
    }

    private bool ErrorLog()
    {
        bool errorFound = false;
        boardText.text = "";
        //set error messages 
        if (errorType[0])//0: no battery, 1: unconnected battery/components, 2: only batteries, 3: no resistors? - required
        {
            errorFound = true;
            boardText.text += "Your circuit is missing a battery. Without a voltage source, it will not function\n";
            //Debug.Log("Your circuit is missing a battery. Without a voltage source, it will not function");
        }
        if (errorType[1])
        {
            errorFound = true;
            boardText.text +=  "There are unconnected components. All components should be fully connected before build\n";
        }
        if (errorType[2])
        {
            errorFound = true;
            boardText.text += "Your circuit has no function. Without sufficient resistance, you can cause serious damage to your power source\n";
        }
        if (errorType[3])
        {
            errorFound = true;
            boardText.text += "Your circuit has no resistance. Without sufficient resistance, you can cause serious damage to your power source\n";
        }
        if (!errorFound)
        {
            boardText.text += "You are good to go!\n";
        }
        //reset
        for (int i = 0; i < errorType.Length; i++)
        {
            errorType[i] = false;
        }
        return errorFound;
    }
}
