using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Managers;

public class OptionsClose : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject OptionsObject;
    [SerializeField] private GameObject homeObject;
    [SerializeField] private GameObject restartObject;

    public void OnPointerClick(PointerEventData eventData)
    {
        OptionsObject.SetActive(false);

        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            GameManager.Instance.OptionsPanelOnOff();
        }
    }
}
