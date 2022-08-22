using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class PlayerController : MonoBehaviour
{
    private CameraRotate cameraRotate;

    private void Start()
    {
        GameManager.Instance.Player = this;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        cameraRotate = GetComponent<CameraRotate>();
    }

    private void Update()
    {
        UpdateRotate();
    }

    private void UpdateRotate()
    {
        if (GameManager.Instance.IsGamePaused) return;
        
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        cameraRotate.UpdateRotate(mouseX, mouseY);
    }
}
