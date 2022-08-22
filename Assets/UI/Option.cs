using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class Option : MonoBehaviour
{
    [SerializeField] private GameObject optionCanvas;
    [SerializeField] private GameObject homeObject;
    [SerializeField] private GameObject restartObject;

    public void EnterOption()
    {
        GameManager.Instance.IsOptionCanvasOn = true;

        optionCanvas.SetActive(true);

        gameObject.SetActive(false);
        homeObject.SetActive(false);
        restartObject.SetActive(false);
    }
}
