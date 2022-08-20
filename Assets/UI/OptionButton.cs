using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OptionButton : SideBarControl, IPointerClickHandler
{
    [SerializeField]
    private GameObject OptionsObject;

    public void OnPointerClick(PointerEventData eventData)
    {
        OptionsObject.SetActive(true);
    }
}
