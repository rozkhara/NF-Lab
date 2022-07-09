using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunController
{
    public Gun Holder { get; private set; }

    public bool IsResourceLoaded { get; private set; }

    /// <summary>
    /// 연사 속도
    /// </summary>
    public abstract float FiringRate { get; }

    /// <summary>
    /// 탄창 내 총알의 수
    /// </summary>
    public abstract int ReloadBulletCount { get; }

    private bool isReloaded;

    protected GameObject resource;

    public void AttachThis(Gun gun)
    {
        Holder = gun;

        Holder.FiringRateCounter = FiringRate;
        Holder.CurrentBulletCount = ReloadBulletCount;

        OnAttached();

        Holder.StartCoroutine(Load());
    }

    public void DetachThis()
    {
        Holder = null;

        OnDetached();
    }

    public abstract void OnUpdate();

    protected abstract void OnAttached();

    protected abstract void OnDetached();

    private IEnumerator Load()
    {
        IsResourceLoaded = false;

        yield return LoadResources();

        var t = resource.transform;
        t.parent = Holder.transform;

        IsResourceLoaded = true;
    }

    protected abstract IEnumerator LoadResources();
}
