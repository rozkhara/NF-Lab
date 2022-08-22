using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;
using UnityEngine.SceneManagement;

public class CrossHair : MonoBehaviour
{
    [SerializeField] private Image crossHair;
    [SerializeField] private Button[] crossHairButtons;
    [SerializeField] private Button[] colorButtons;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            crossHair.sprite = UIManager.Instance.crossHairSprite;
            crossHair.gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, UIManager.Instance.CrossHairScale);
            crossHair.gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, UIManager.Instance.CrossHairScale);
            crossHair.color = colorButtons[UIManager.Instance.CrossHairIdx].GetComponent<Image>().color;
        }

        for (int i = 0; i < crossHairButtons.Length; i++)
        {
            int idx = i;
            crossHairButtons[idx].onClick.AddListener(() => SetCrossHairType(idx));
            colorButtons[idx].onClick.AddListener(() => SetCrossHairColor(idx));
        }
    }

    private void SetCrossHairType(int idx)
    {
        crossHair.sprite = crossHairButtons[idx].transform.GetChild(0).GetComponent<Image>().sprite;

        UIManager.Instance.crossHairSprite = crossHair.sprite;

        switch(idx)
        {
            case 0:
                crossHair.gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 20f);
                crossHair.gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 20f);

                UIManager.Instance.CrossHairScale = 20f;
                break;
            case 1:
                crossHair.gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 80f);
                crossHair.gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 80f);

                UIManager.Instance.CrossHairScale = 90f;
                break;
            case 2:
            case 3:
                crossHair.gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 30f);
                crossHair.gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 30f);

                UIManager.Instance.CrossHairScale = 30f;
                break;
            case 4:
                crossHair.gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 13f);
                crossHair.gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 13f);

                UIManager.Instance.CrossHairScale = 13f;
                break;
        }
    }

    private void SetCrossHairColor(int idx)
    {
        crossHair.color = colorButtons[idx].GetComponent<Image>().color;

        UIManager.Instance.CrossHairIdx = idx;
    }
}
