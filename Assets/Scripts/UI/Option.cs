using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    [SerializeField] private GameObject optionCanvas;

    public void EnterOption()
    {
        GameManager.Instance.IsOptionCanvasOn = true;

        optionCanvas.SetActive(true);

        gameObject.SetActive(false);
    }
}
