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

    /// <summary>
    /// 분열되어 나온 타겟인지
    /// </summary>
    public bool IsParticle { get; set; }

    private readonly List<TargetController> targets = new List<TargetController>();

    private readonly List<TargetController> particles = new List<TargetController>();

    private readonly List<int> indices = new List<int>();

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

    public void Fission(Vector3 pos)
    {
        Holder.gameObject.SetActive(false);
        TargetSpawner.targetPool[Name].Enqueue(this);

        // 수소는 분열 안 함
        if (Mass == 1) return;

        // 후보군 원자 선별
        foreach (var targetType in TargetSpawner.targetTypes)
        {
            if (targetType.Mass < Mass) targets.Add(targetType);
        }

        int mass = Mass;

        // 분열
        while (mass > 0)
        {
            int idx = Random.Range(0, targets.Count);

            if (targets[idx].Mass > mass)
            {
                targets.RemoveAt(idx);
                continue;
            }

            var target = TargetSpawner.targetPool[targets[idx].Name].Dequeue();
            target.IsParticle = true;

            particles.Add(target);

            mass -= targets[idx].Mass;
        }

        for (int i = 0; i < 8; i++)
        {
            indices.Add(i);
        }

        for (int i = 0; i < particles.Count; i++)
        {
            var idx = Random.Range(0, indices.Count);

            var axis = (Holder.transform.position - pos).normalized;
            var direction = Quaternion.AngleAxis(45f * indices[idx], axis) * (axis + Vector3.ProjectOnPlane(Vector3.up, axis));

            indices.RemoveAt(idx);

            particles[i].Holder.transform.position = Holder.transform.position + direction;
            particles[i].Holder.gameObject.SetActive(true);

            particles[i].Holder.GetForce(direction);
        }
    }
}
