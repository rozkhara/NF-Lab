using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class OptionsClose : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject OptionsObject;

    public void OnPointerClick(PointerEventData eventData)
    {
        OptionsObject.SetActive(false);

        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            GameManager.Instance.OptionsPanelOnOff();
        }
    }
}
