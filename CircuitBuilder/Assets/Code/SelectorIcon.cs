using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class SelectorIcon : MonoBehaviour
{
    public GameObject dragTile;//a reference to the specific drag tile that this selector uses
    public GameObject controller;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Select();
    }

    void Select()//what to do if the user clicks the tile
    {
        if (Input.GetMouseButtonDown(0) && GetComponent<TileHover>().HoverTest())
        {
            //Instantiate(Object original, Vector3 position, Quaternion rotation, Transform parent);
            GameObject vroom = Instantiate(dragTile, transform.position, transform.rotation, transform.parent.transform.parent);//parent must be out of the layout so it can be freely dragged by mouse
            vroom.GetComponent<RectTransform>().sizeDelta = transform.parent.GetComponent<GridLayoutGroup>().cellSize;//dynamically set size of dragtile to fit that of the cell
            vroom.GetComponent<Tile>().controller = controller;//give the tile a reference to the controller
        }
    }
}
