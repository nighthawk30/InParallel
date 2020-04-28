using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class SelectorTile : MonoBehaviour
{
    public DragTile dragTile;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Select();
    }

    void Select()
    {
        if (Input.GetMouseButtonDown(0) && GetComponent<TileHover>().HoverTest())
        {
            DragTile vroom = Instantiate(dragTile);
            vroom.transform.SetParent(transform.parent.transform.parent, false);
        }
    }
}
