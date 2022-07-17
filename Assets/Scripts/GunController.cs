using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunController
{
    public Gun Holder { get; private set; }

    public bool IsResourceLoaded { get; private set; }

    protected GameObject resource;

    public void AttachThis(Gun gun)
    {
        Holder = gun;

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
