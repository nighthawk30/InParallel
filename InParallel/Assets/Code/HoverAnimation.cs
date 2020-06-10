using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverAnimation : MonoBehaviour
{
    public Sprite[] anim = new Sprite[8];
    float timeLoop = 0;
    int animIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        //r = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timeLoop += 10*Time.deltaTime;
        if (timeLoop > 8)
        {
            timeLoop = 0;
        }
        animIndex = (int)(Mathf.Floor(timeLoop));
        GetComponent<Image>().sprite = anim[animIndex];
    }
}
