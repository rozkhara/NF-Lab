using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private bool isReloaded = false;

    /// <summary>
    /// 연사 속도
    /// </summary>
    public float FiringRate { get; set; }

    public float FiringRateCounter { get; set; }

    /// <summary>
    /// 탄창 내 최대 총알의 수
    /// </summary>
    public int ReloadBulletCount { get; set; }

    /// <summary>
    /// 현재 탄창 내 총알의 수
    /// </summary>
    public int CurrentBulletCount { get; set; }

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
        if (controller == null) return;

        controller.OnUpdate();

        GunFireRateCalc();
        TryFire();
        TryReload();
    }

    private void OnDestroy()
    {
        Controller = null;
    }

    private void GunFireRateCalc()
    {
        if (FiringRateCounter > 0) FiringRateCounter -= Time.deltaTime;
    }

    private void TryFire()
    {
        if (Input.GetButton("Fire1") && FiringRateCounter <= 0 && !isReloaded) Fire();
    }

    private void Fire()
    {
        if (!isReloaded)
        {
            if (CurrentBulletCount > 0) Shoot();
            else StartCoroutine(ReloadCoroutine());
        }
    }

    private void Shoot()
    {
        CurrentBulletCount--;
        FiringRateCounter = FiringRate;

        Hit();

        StartCoroutine(RetroActionCoroutine());
    }

    private void Hit()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, 100f))
        {
            Debug.Log(hitInfo.transform.name);
        }
    }

    private void TryReload()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isReloaded && CurrentBulletCount < ReloadBulletCount)
        {
            StartCoroutine(ReloadCoroutine());
        }
    }

    private IEnumerator ReloadCoroutine()
    {
        isReloaded = true;

        CurrentBulletCount = ReloadBulletCount;

        yield return new WaitForSeconds(2f);

        isReloaded = false;
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
