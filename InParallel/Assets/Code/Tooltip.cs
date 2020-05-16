using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField] Text tooltipText;
    [SerializeField] Image tooltipImage;

    void Update()
    {
        transform.position = Input.mousePosition;
    }

    public void ShowTooltip(string tooltipString)
    {
        transform.position = Input.mousePosition;//avoids frame of moving ui
        gameObject.SetActive(true);
        tooltipText.text = tooltipString;
        float paddingSize = 4f;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + paddingSize * 2, tooltipText.preferredHeight + paddingSize * 2);
        tooltipImage.GetComponent<RectTransform>().sizeDelta = backgroundSize;
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }
}
