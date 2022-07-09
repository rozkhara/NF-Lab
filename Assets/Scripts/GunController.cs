using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunController
{
    public Gun Holder { get; private set; }

    /// <summary>
    /// 연사 속도
    /// </summary>
    public abstract float FiringRate { get; }

    /// <summary>
    /// 재장전 하는 총알의 수
    /// </summary>
    public abstract int ReloadBulletCount { get; }

    private bool isReloaded;

    public void AttachThis(Gun gun)
    {
        Holder = gun;

        OnAttached();
    }

    public void DetachThis()
    {
        Holder = null;

        OnDetached();
    }

    public void OnUpdate()
    {
    }

    protected abstract void OnAttached();

    protected abstract void OnDetached();
}
