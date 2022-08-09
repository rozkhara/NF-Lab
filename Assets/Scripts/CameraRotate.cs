using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    /// <summary>
    /// 수직 마우스 감도
    /// </summary>
    public float XSensitivity { get; set; }

    /// <summary>
    /// 수평 마우스 감도
    /// </summary>
    public float YSensitivity { get; set; }

    private float limitMinX = -90;
    private float limitMaxX = 90;
    private float limitMinY = -90;
    private float limitMaxY = 90;

    private float eulerAngleX;
    private float eulerAngleY;

    private void Start()
    {
        GameManager.Instance.CamRotate = this;

        XSensitivity = 3f;
        YSensitivity = 5f;
    }

    public void UpdateRotate(float mouseX, float mouseY)
    {
        eulerAngleY += mouseX * YSensitivity;
        eulerAngleX -= mouseY * XSensitivity;

        eulerAngleX = ClampAngle(eulerAngleX, limitMinX, limitMaxX);
        eulerAngleY = ClampAngle(eulerAngleY, limitMinY, limitMaxY);

        transform.rotation = Quaternion.Euler(eulerAngleX, eulerAngleY, 0);
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f) angle += 360f;
        if (angle > 360f) angle -= 360f;

        return Mathf.Clamp(angle, min, max);
    }
}
