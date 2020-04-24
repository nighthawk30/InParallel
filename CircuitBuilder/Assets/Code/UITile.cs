using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITile : MonoBehaviour
{
    //button event stuff
    //https://docs.unity3d.com/2018.1/Documentation/ScriptReference/UI.Button-onClick.html
    //https://docs.unity3d.com/2017.1/Documentation/ScriptReference/UI.Button-onClick.html

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Button>().onClick.AddListener(TaskOnClick);//add the on click listener to this button
    }
    /*
    void TaskOnClick()
    {
        Debug.Log("click");
    }
    */

    void OnPointerClick()
    {
        Debug.Log("click");
    }

    void OnPointerUp()
    {
        Debug.Log("release");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
