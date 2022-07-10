using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargetController
{
    public Target Holder { get; private set; }

    public void AttachThis(Target target)
    {
        Holder = target;

        Holder.Life = 1;

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

    public abstract void Fission();
}
