using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargetController
{
    public Target Holder { get; private set; }

    public void AttachThis(Target target)
    {
        Holder = target;
    }

    public void DetachThis()
    {
        Holder = null;
    }

    public abstract void OnUpdate();

    public void GetHit()
    {
        Holder.Life--;
    }
}
