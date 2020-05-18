using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class SelectorIcon : MonoBehaviour
{
    public GameObject tile;//a reference to the specific  tile that this selector uses
    public GameObject controller;
    public string iconType;
    bool enter = false;//keeps track of when the mouse enters the icon to show the tool tip

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && GetComponent<Hover>().HoverTest())//clicked
        {
            //Instantiate(Object original, Vector3 position, Quaternion rotation, Transform parent);
            controller.GetComponent<Controller>().wireSwitch = false;//turn off wiring toggle
            GameObject vroom = Instantiate(tile, transform.position, transform.rotation, transform.parent.transform.parent.transform.parent.GetChild(transform.parent.transform.parent.GetSiblingIndex() + 1));
            vroom.transform.SetSiblingIndex(transform.parent.GetSiblingIndex() + 1);//put tiles at top so drawing order is followed
            vroom.GetComponent<RectTransform>().sizeDelta = transform.parent.GetComponent<GridLayoutGroup>().cellSize;//dynamically set size of dragtile to fit that of the cell
            vroom.GetComponent<Tile>().controller = controller;//give the tile a reference to the controller
        }

        //tooltip 
        if (GetComponent<Hover>().HoverTest() && !enter)
        {
            if (!Input.GetMouseButton(0))
            {
                enter = true;
                controller.GetComponent<Controller>().toolTip.GetComponent<Tooltip>().ShowTooltip(iconType);
            }
        }
        if ((enter && !GetComponent<Hover>().HoverTest()) || Input.GetMouseButton(0))
        {
            enter = false;
            controller.GetComponent<Controller>().toolTip.GetComponent<Tooltip>().HideTooltip();
        }
    }
}
