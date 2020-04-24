using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class TileHover : MonoBehaviour
{
    public Sprite BuildTile;
    public Sprite HoverTile;
    bool hovering;
    float screenRatio;

    Vector2 mouseGamePostion = new Vector2(0, 0);
    Vector2 cursorDistance = new Vector2(0, 0);

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        screenRatio = 288 / ((float)(Screen.height));
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        mouseGamePostion.x = (Input.mousePosition.x - Screen.width / 2) * screenRatio;
        mouseGamePostion.y = (Input.mousePosition.y - Screen.height / 2) * screenRatio;

        cursorDistance.x = Mathf.Abs(mouseGamePostion.x - transform.position.x);
        cursorDistance.y = Mathf.Abs(mouseGamePostion.y - transform.position.y);

        if (!hovering && cursorDistance.x < 32 && cursorDistance.y < 32)//32 bc the object is scaled by 2
        {
            spriteRenderer.sprite = HoverTile;
            hovering = true;
        }
        
        if (hovering && (cursorDistance.x > 32 || cursorDistance.y > 32))
        {
            spriteRenderer.sprite = BuildTile;
            hovering = false;
        }
               
    }
}
