using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spawners;

public abstract class TargetController
{
    public Target Holder { get; private set; }

    /// <summary>
    /// 타겟의 이름 (프리팹 이름과 동일해야 함)
    /// </summary>
    public abstract string Name { get; }

    /// <summary>
    /// 타겟의 질량
    /// </summary>
    public abstract int Mass { get; }

    public bool IsParticle { get; private set; }

    private readonly List<TargetController> targets = new List<TargetController>();

    private readonly List<TargetController> particles = new List<TargetController>();

    public bool IsResourceLoaded { get; private set; }

    protected GameObject resource;

    private Rigidbody rb;

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

        rb = Holder.transform.GetChild(0).GetComponent<Rigidbody>();

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

        // 수소는 분열 안 함
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
            target.IsParticle = true;

            particles.Add(target);

            mass -= targets[tmp].Mass;
        }

        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Holder.transform.position = Holder.transform.position + new Vector3(-particles.Count / 2 + i, 0f, 1f);
            particles[i].Holder.gameObject.SetActive(true);

            particles[i].rb.AddForce(new Vector3(-particles.Count / 2 + i, 0f, 1f) * 7f, ForceMode.Impulse);
        }
    }
}
