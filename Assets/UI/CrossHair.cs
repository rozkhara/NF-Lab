using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour
{
    [SerializeField] private Image crossHair;
    [SerializeField] private Button[] buttons;

    private void Awake()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int idx = i;
            buttons[idx].onClick.AddListener(() => SetCrossHairType(idx));
        }
    }

    private void SetCrossHairType(int idx)
    {
        crossHair.sprite = buttons[idx].transform.GetChild(0).GetComponent<Image>().sprite;

        switch(idx)
        {
            case 0:
                crossHair.gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 20f);
                crossHair.gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 20f);
                break;
            case 1:
                crossHair.gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 90f);
                crossHair.gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 90f);
                break;
            case 2:
            case 3:
                crossHair.gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 30f);
                crossHair.gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 30f);
                break;
            case 4:
                crossHair.gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 13f);
                crossHair.gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 13f);
                break;
        }
    }
}
