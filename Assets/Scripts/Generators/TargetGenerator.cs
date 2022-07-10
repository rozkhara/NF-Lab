using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spawners;

public abstract class TargetGenerator
{
    public Queue<TargetController> targets = new Queue<TargetController>();

    public GameObject Pool { get; set; }

    public Target Holder { get; protected set; }

    public bool IsResourceLoaded { get; private set; }

    protected GameObject resource;

    public Queue<TargetController> getTargets()
    {
        return targets;
    }

    public abstract void CreateTarget(int count);

    protected IEnumerator Load()
    {
        IsResourceLoaded = false;

        yield return LoadResources();

        var t = resource.transform;
        t.parent = Holder.transform;

        resource.SetActive(false);

        IsResourceLoaded = true;
    }

    protected abstract IEnumerator LoadResources();
}
