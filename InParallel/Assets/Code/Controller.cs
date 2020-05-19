using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    //run time variables
    public GameObject toolTip;
    bool corunning = false;
    public GameObject currentSelection = null;//what the user is selecting this a way of passing references between objects
    public bool wireSwitch = false;//is the wire toggle on
    public GameObject wireConnection;//the wire itself
    public GameObject[] board;
    public GameObject output;

    //the setup class for each level runs the start
    void Start()
    {
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
        if (Input.GetMouseButtonUp(0) && wireConnection == null)//intervals and you arent placing wires
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
