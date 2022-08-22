using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SideBarControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private readonly int deltaX = 600;

    private RectTransform rt;

    private Vector3 defaultPos;
    private Vector3 extendedPos;

    protected readonly float animationTime = 0.5f;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();

        defaultPos = rt.localPosition;

        extendedPos = new Vector3(defaultPos.x - deltaX, defaultPos.y, defaultPos.z);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(Slide(0, animationTime));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StartCoroutine(Slide(1, animationTime));
    }

    IEnumerator Slide(int mode, float time)
    {
        float elapsedTime = 0f;
        Vector3 startPos = rt.localPosition;

        if (mode == 0)
        {
            while (elapsedTime < time)
            {
                elapsedTime += Time.deltaTime;
                rt.localPosition = Vector3.Lerp(startPos, extendedPos, elapsedTime / time);
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            while (elapsedTime < time)
            {
                elapsedTime += Time.deltaTime;
                rt.localPosition = Vector3.Lerp(startPos, defaultPos, elapsedTime / time);
                yield return new WaitForEndOfFrame();
            }
        }

        yield return null;
    }
}
