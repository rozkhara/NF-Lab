using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreenObjectManipulation : MonoBehaviour
{
    private Quaternion prevRotation = Quaternion.identity;
    private Quaternion newRotation = Quaternion.identity;

    private GameObject MainObject;
    private float timeStamp = 0.0f;
    private float timeLimit = 0.0f;

    private void Start()
    {
        MainObject = GameObject.Find("MainObject");
        StartCoroutine(GetNewRotation());
    }

    private void Update()
    {
        MainObject.transform.rotation = Quaternion.Slerp(prevRotation, newRotation, timeStamp / timeLimit);
        timeStamp += Time.deltaTime;
        timeStamp = Mathf.Clamp(timeStamp, 0.0f, timeLimit);
        if (timeStamp >= timeLimit)
        {
            timeStamp = 0.0f;
            timeLimit = Random.Range(1.0f, 3.0f);
            StartCoroutine(GetNewRotation());
        }
    }

    private IEnumerator GetNewRotation()
    {
        prevRotation = newRotation;
        newRotation = Random.rotation;
        yield return null;
    }

}
