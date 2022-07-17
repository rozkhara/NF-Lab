using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;

public sealed class Gun : MonoBehaviour
{
    /// <summary>
    /// 연사 속도
    /// </summary>
    public float FiringRate { get; set; }

    private float firingRateCounter;

    private Vector3 originPos;

    private RaycastHit hitInfo;

    private Camera cam;

    private GunController controller;

    public GunController Controller
    {
        get => controller;
        set
        {
            controller?.DetachThis();
            controller = value;
            controller?.AttachThis(this);
        }
    }

    private void Awake()
    {
        originPos = transform.localPosition;
        cam = Camera.main;
    }

    private void Update()
    {
        // 관련 리소스가 로드되지 않은 상태에선 행동을 멈춘다.
        if (!(controller is { IsResourceLoaded: true })) return;

        controller.OnUpdate();

        GunFireRateCalc();
        TryFire();
    }

    private void OnDestroy()
    {
        Controller = null;
    }

    private void GunFireRateCalc()
    {
        if (firingRateCounter > 0) firingRateCounter -= Time.deltaTime;
    }

    private void TryFire()
    {
        if (Input.GetButton("Fire1") && firingRateCounter <= 0) Fire();
    }

    private void Fire()
    {
        firingRateCounter = FiringRate;

        Hit();

        StartCoroutine(RetroActionCoroutine());
    }

    private void Hit()
    {
        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hitInfo))
        {
            Debug.Log(hitInfo.transform.name);

            if (hitInfo.transform.tag == "Target") hitInfo.transform.GetComponentInParent<Target>().GetHit();
        }
    }

    private IEnumerator RetroActionCoroutine()
    {
        Vector3 recoilBack = new Vector3(0.3f, originPos.y, originPos.z);

        transform.localPosition = originPos;

        // 반동 시작
        while (transform.localPosition.x <= 0.3f - 0.02f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, recoilBack, 0.4f);
            yield return null;
        }

        // 원위치
        while (transform.localPosition != originPos)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, originPos, 0.1f);
            yield return null;
        }
    }
}
