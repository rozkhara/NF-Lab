using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Managers;

public class MouseSensitivityX : MonoBehaviour
{
    private Slider slider;

    private TextMeshProUGUI valueText;

    public void Start()
    {
        slider = GetComponent<Slider>();

        valueText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        slider.value = UIManager.Instance.XSensitivity;
        valueText.text = slider.value.ToString("F1");
    }

    public void ChangeX()
    {
        if (UIManager.Instance.CamRotate != null) UIManager.Instance.CamRotate.XSensitivity = slider.value;
        
        UIManager.Instance.XSensitivity = slider.value;

        valueText.text = slider.value.ToString("F1");
    }
}
