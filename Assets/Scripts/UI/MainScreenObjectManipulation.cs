using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreenObjectManipulation : MonoBehaviour
{
    private Quaternion prevRotation = Quaternion.identity;
    private Quaternion newRotation = Quaternion.identity;

    private GameObject mainObject;

    private float timeStamp = 0f;
    private float timeLimit = 0f;

    private void Start()
    {
        mainObject = GameObject.Find("MainObject");

        GetNewRotation();
    }

    private void Update()
    {
        mainObject.transform.rotation = Quaternion.Slerp(prevRotation, newRotation, timeStamp / timeLimit);

        timeStamp += Time.deltaTime;
        timeStamp = Mathf.Clamp(timeStamp, 0.0f, timeLimit);

        if (timeStamp >= timeLimit)
        {
            timeStamp = 0.0f;
            timeLimit = Random.Range(1.0f, 3.0f);

            GetNewRotation();
        }
    }

    private void GetNewRotation()
    {
        prevRotation = newRotation;

        newRotation = Random.rotation;
    }

}
