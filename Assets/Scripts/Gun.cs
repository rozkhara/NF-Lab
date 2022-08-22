using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using Managers;

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
        if (!GameManager.Instance.IsGamePaused && Input.GetButton("Fire1") && firingRateCounter <= 0) Fire();
    }

    private void Fire()
    {
        firingRateCounter = FiringRate;

        int x = Random.Range(1, 3);
        SoundManager.Instance.PlaySFXSound("gunFire" + x, 0.4f);

        Hit();

        StartCoroutine(RetroActionCoroutine());
    }

    private void Hit()
    {
        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hitInfo))
        {
            var pos = hitInfo.transform.position;

            var dir = pos - new Vector3(0f, 3.1f, pos.z);
            dir.z += dir.magnitude * 1.3f;
            dir = dir.normalized;

            if (hitInfo.transform.tag == "Target")
            {
                hitInfo.transform.GetComponentInParent<Target>().GetHit(pos - dir);

                UIManager.Instance.IncreaseStreak();
            }
            else UIManager.Instance.ResetStreak();
        }
    }

    private IEnumerator RetroActionCoroutine()
    {
        Vector3 recoilBack = new Vector3(0.3f, originPos.y, originPos.z);

        // 반동 시작
        while (transform.localPosition.x <= 0.3f - 0.01f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, recoilBack, 100f * Time.deltaTime);
            yield return null;
        }

        // 원위치
        while (transform.localPosition.x > originPos.x + 0.01f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, originPos, 100f * Time.deltaTime);
            yield return null;
        }

        transform.localPosition = originPos;
    }
}
