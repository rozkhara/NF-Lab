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
    ///  타겟의 점수
    /// </summary>
    public abstract int Score { get; }

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

        ScoreManager.Instance.IncreaseScore(Score);

        int x = Random.Range(1, 4);
        SoundManager.Instance.PlaySFXSound("targetHit" + x, 0.2f);

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

        #region Debug
        /*
        for (int i = 0; i < 8; i++)
        {
            var target = TargetSpawner.targetPool["TargetH"].Dequeue();
            target.IsParticle = true;

            particles.Add(target);
        }
        */
        #endregion

        for (int i = 0; i < 8; i++)
        {
            indices.Add(i);
        }

        Vector3 axis = (Holder.transform.position - pos).normalized;
        int key = -1;

        // 벽 밖으로 나갈 것 같으면 생성 위치를 틀어줌
        if (!CheckParticleInArea(pos, ref key))
        {
            switch (key)
            {
                // 좌우 벽 밖으로 나갈 때
                case 0:
                    axis.x = -axis.x;
                    break;
                // 상하 벽 밖으로 나갈 때
                case 1:
                    axis.y = -axis.y;
                    break;
                // 꼭짓점 밖으로 나갈 때
                case 2:
                    axis.x = -axis.x;
                    axis.y = -axis.y;
                    break;
            }
        }

        // 첫 번째 파티클은 충격의 진행 방향으로
        particles[0].Holder.transform.position = Holder.transform.position + axis * 1.1f;
        particles[0].Holder.gameObject.SetActive(true);

        particles[0].Holder.GetForce(axis);

        var normal = Vector3.ProjectOnPlane(Vector3.up, axis).normalized;

        for (int i = 1; i < particles.Count; i++)
        {
            var idx = Random.Range(0, indices.Count);
            var direction = Quaternion.AngleAxis(45f * indices[idx], axis) * (axis * 1.1f + normal * 1.35f);

            indices.RemoveAt(idx);

            particles[i].Holder.transform.position = Holder.transform.position + direction;
            particles[i].Holder.gameObject.SetActive(true);

            particles[i].Holder.GetForce(direction.normalized);
        }
    }

    private bool CheckParticleInArea(Vector3 pos, ref int key)
    {
        var axis = (Holder.transform.position - pos).normalized;
        var normal = Vector3.ProjectOnPlane(Vector3.up, axis).normalized;

        for (int i = 0; i < 8; i++)
        {
            var direction = Quaternion.AngleAxis(45f * i, axis) * (axis * 1.1f + normal * 1.35f);
            var vec = Holder.transform.position + direction;

            if ((vec.x <= -2.45f || vec.x >= 2.45f) && vec.y > 0.65f && vec.y < 6.45f) key = key == 1 ? 2 : 0;

            if (vec.x > -2.45f && vec.x < 2.45f && (vec.y <= 0.65f || vec.y >= 6.45f)) key = key == 0 ? 2 : 1;

            if ((vec.x <= -2.45f || vec.x >= 2.45f) && (vec.y <= 0.65f || vec.y >= 6.45f)) key = 2;

            if (key == 2) return false;
        }

        if (key == -1) return true;
        else return false;
    }

    public bool CheckParticleRoute(Vector3 axis)
    {
        if (axis.z <= 0) return false;

        var normal = Vector3.ProjectOnPlane(Vector3.up, axis).normalized;

        for (int i = 0; i < 8; i++)
        {
            var direction = Quaternion.AngleAxis(45f * i, axis) * (axis * 1.1f + normal * 1.35f);

            if (direction.z <= 0) return false;
        }

        return true;
    }
}
