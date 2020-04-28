using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class TileHover : MonoBehaviour
{
    //public Sprite BuildTile;
    public Sprite HoverTile;
    public GameObject moveTile;

    bool hovering;
    float screenRatio;

    Vector2 cursorDistance = new Vector2(0, 0);

    //private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        screenRatio = 160 / ((float)(Screen.height));
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        hovering = HoverTest();
        if (hovering)
        {
            if (Input.GetMouseButtonDown(0))
            {
                movePrefab moveitmoveit = Instantiate(moveTile);
            }
        }
    }

    bool HoverTest()
    {
        cursorDistance.x = Mathf.Abs((Input.mousePosition.x - Screen.width / 2) * screenRatio - transform.position.x);
        cursorDistance.y = Mathf.Abs((Input.mousePosition.y - Screen.height / 2) * screenRatio - transform.position.y);
        if (cursorDistance.x < 32 && cursorDistance.y < 32)//32 bc the object is scaled by 2
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
