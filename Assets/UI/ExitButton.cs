using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ExitButton : SideBarControl, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.LogError("QUIT");

        Application.Quit();
    }
}
