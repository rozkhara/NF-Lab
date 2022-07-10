using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    private float rotCamXAxisSpeed; // 수직
    private float rotCamYAxisSpeed; // 수평

    private float limitMinX = -90;
    private float limitMaxX = 90;
    private float limitMinY = -90;
    private float limitMaxY = 90;

    private float eulerAngleX;
    private float eulerAngleY;

    private void Awake()
    {
        rotCamXAxisSpeed = 3f;
        rotCamYAxisSpeed = 5f;
    }

    public void UpdateRotate(float mouseX, float mouseY)
    {
        eulerAngleY += mouseX * rotCamYAxisSpeed;
        eulerAngleX -= mouseY * rotCamXAxisSpeed;

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
