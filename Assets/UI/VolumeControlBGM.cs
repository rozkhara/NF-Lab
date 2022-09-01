using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Managers;
using UnityEngine.EventSystems;

public class VolumeControlBGM : MonoBehaviour
{
    private Slider slider;

    private TextMeshProUGUI valueText;

    public void Start()
    {
        slider = GetComponent<Slider>();

        valueText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        slider.value = SoundManager.Instance.GetBGMVolume() * 100;
        valueText.text = ((int)slider.value).ToString();
    }

    public void ChangeBGM()
    {
        SoundManager.Instance.SetBGMVolume(slider.value / 100);

        valueText.text = ((int)slider.value).ToString();
    }
}