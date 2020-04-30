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
            GameObject vroom = Instantiate(dragTile, transform.position, transform.rotation, transform.parent.transform.parent);//Instantiate(Object original, Vector3 position, Quaternion rotation, Transform parent);
            //vroom.transform.localScale = new Vector3(1, 1, 1);
            //vroom.GetComponent<RectTransform>().sizeDelta = vroom.transform.parent.GetComponent<GridLayoutGroup>().cellSize;
            vroom.GetComponent<DragTile>().controller = controller;//give the tile a reference to the controller
            controller.GetComponent<Controller>().currentSelection = this.gameObject;//set the object that the controller creates - rn just copies sprite
        }
    }
}
