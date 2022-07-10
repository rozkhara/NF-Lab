using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spawners;

public abstract class TargetGenerator
{
    public Queue<TargetController> targets { get; } = new Queue<TargetController>();

    public GameObject Pool { get; set; }

    public Target Holder { get; protected set; }

    public bool IsResourceLoaded { get; private set; }

    public abstract string Name { get; }

    protected GameObject resource;

    public abstract void CreateTarget(int count);

    protected IEnumerator Load(Target holder)
    {
        IsResourceLoaded = false;

        yield return LoadResources();

        var t = resource.transform;
        t.parent = holder.transform;

        holder.gameObject.SetActive(false);

        IsResourceLoaded = true;
    }

    protected abstract IEnumerator LoadResources();
}
