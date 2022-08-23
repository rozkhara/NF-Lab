using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Managers;
using UnityEngine.EventSystems;

public class VolumeControl : MonoBehaviour, IPointerUpHandler
{
    private Slider slider;

    private TextMeshProUGUI valueText;

    public void Start()
    {
        slider = GetComponent<Slider>();

        valueText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        valueText.text = ((int)slider.value).ToString();
    }

    public void ChangeSFX()
    {
        SoundManager.Instance.SetSFXVolume(slider.value / 100);

        valueText.text = ((int)slider.value).ToString();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SoundManager.Instance.PlaySFXSound("gunFire1", 0.4f);
    }
}
