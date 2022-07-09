using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private bool isReloaded = false;

    private float firingRateCounter;

    /// <summary>
    /// 현재 탄창 내 총알의 수
    /// </summary>
    private int currentBulletCount;

    private Vector3 originPos;

    private RaycastHit hitInfo;

    private GunController controller;

    private Camera cam;

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
        firingRateCounter = Controller.FiringRate;
        originPos = transform.localPosition;
        cam = transform.parent.GetChild(0).GetComponent<Camera>();
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
        if (firingRateCounter > 0) firingRateCounter -= Time.deltaTime;
    }

    private void TryFire()
    {
        if (Input.GetButton("Fire1") && Controller.FiringRate <= 0 && isReloaded) Fire();
    }

    private void Fire()
    {
        if (!isReloaded)
        {
            if (currentBulletCount > 0) Shoot();
            else StartCoroutine(ReloadCoroutine());
        }
    }

    private void Shoot()
    {
        currentBulletCount--;
        firingRateCounter = Controller.FiringRate;

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
        if (Input.GetKeyDown(KeyCode.R) && !isReloaded && currentBulletCount < Controller.ReloadBulletCount)
        {
            StartCoroutine(ReloadCoroutine());
        }
    }

    private IEnumerator ReloadCoroutine()
    {
        isReloaded = true;

        currentBulletCount = Controller.ReloadBulletCount;

        yield return new WaitForSeconds(2f);

        isReloaded = false;
    }

    private IEnumerator RetroActionCoroutine()
    {
        Vector3 recoilBack = new Vector3(1f, originPos.y, originPos.z);

        transform.localPosition = originPos;

        // 반동 시작
        while (transform.localPosition.x <= 1f - 0.02f)
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
