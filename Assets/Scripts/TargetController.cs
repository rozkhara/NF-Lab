using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spawners;

public abstract class TargetController
{
    public Target Holder { get; private set; }

    public abstract string Name { get; }

    public abstract int Mass { get; }

    protected readonly List<TargetController> targets = new List<TargetController>();

    public bool IsResourceLoaded { get; private set; }

    protected GameObject resource;

    public void AttachThis(Target target)
    {
        Holder = target;

        Holder.Life = 1;

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

        Holder.gameObject.SetActive(false);

        IsResourceLoaded = true;
    }

    protected abstract IEnumerator LoadResources();

    public void Fission()
    {
        Holder.gameObject.SetActive(false);

        TargetSpawner.targetPool[Name].Enqueue(this);

        // 후보군 원자 선별
        foreach (var targetType in TargetSpawner.targetTypes)
        {
            if (targetType.Mass < Mass) targets.Add(targetType);
        }

        // 수소의 경우 분열 안 함
        if (Mass == 1) return;

        int mass = Mass;

        // 분열
        while (mass > 0)
        {
            int tmp = Random.Range(0, targets.Count);

            if (targets[tmp].Mass > mass)
            {
                targets.RemoveAt(tmp);
                continue;
            }

            var target = TargetSpawner.targetPool[targets[tmp].Name].Dequeue();

            target.Holder.transform.position = Holder.transform.position + Vector3.forward;
            target.Holder.gameObject.SetActive(true);

            Debug.Log(target.Name);

            mass -= targets[tmp].Mass;
        }
    }
}
