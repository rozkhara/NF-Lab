using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Managers;

public class MouseSensitivityY : MonoBehaviour
{
    private Slider slider;

    private TextMeshProUGUI valueText;

    public void Start()
    {
        slider = GetComponent<Slider>();

        valueText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        slider.value = GameManager.Instance.CamRotate.YSensitivity;
        valueText.text = slider.value.ToString("F1");
    }

    public void ChangeY()
    {
        GameManager.Instance.CamRotate.YSensitivity = slider.value;

        valueText.text = slider.value.ToString("F1");
    }
}
