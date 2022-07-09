using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargetController
{
    public Target Holder { get; private set; }

    public void AttachThis(Target target)
    {
        Holder = target;

        OnAttached();
    }

    public void DetachThis()
    {
        Holder = null;

        OnDetached();
    }

    public abstract void OnUpdate();

    protected abstract void OnAttached();

    protected abstract void OnDetached();

    public void GetHit()
    {
        Holder.Life--;

        if (Holder.Life == 0) Fission(Holder);
    }

    protected abstract void Fission(Target holder);
}
