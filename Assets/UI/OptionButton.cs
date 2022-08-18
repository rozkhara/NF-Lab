using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OptionButton : SideBarControl, IPointerClickHandler
{
    [SerializeField]
    private GameObject Fog;
    [SerializeField]
    private GameObject Panel;

    public void OnPointerClick(PointerEventData eventData)
    {
        Fog.SetActive(true);
        Panel.SetActive(true);
    }
}
