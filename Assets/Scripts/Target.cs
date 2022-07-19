using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spawners;

public sealed class Target : MonoBehaviour
{
    private static readonly HashSet<Target> allTarget = new HashSet<Target>();

    /// <summary>
    /// 타겟이 버틸 수 있는 피격 수
    /// </summary>
    public int Life { get; set; }

    private Rigidbody rb;

    private TargetController controller;

    /// <summary>
    /// 이거를 갈아치워주면 다른 타겟처럼 행동하기 시작함
    /// </summary>
    public TargetController Controller
    {
        get => controller;
        set
        {
            controller?.DetachThis();
            controller = value;
            controller?.AttachThis(this);
        }
    }

    private void Awake()
    {
        allTarget.Add(this);

        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!(controller is { IsResourceLoaded: true })) return;

        Move();
        Disappear();
        GameOver();

        controller.OnUpdate();
    }

    private void OnDestroy()
    {
        Controller = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GetHit();

        TargetSpawner.targetPool[controller.Name].Enqueue(controller);
        gameObject.SetActive(false);
    }

    private static void UnloadResources()
    {
        allTarget.Clear();
    }

    private void Move()
    {
        if (!controller.IsParticle) transform.Translate(Vector3.back * 1f * Time.deltaTime);
    }

    private void Disappear()
    {
        var pos = transform.position;

        if (pos.x < -7f || pos.x > 7f || pos.y < 1f || pos.y > 11f || pos.z > 20f)
        {
            TargetSpawner.targetPool[controller.Name].Enqueue(controller);
            gameObject.SetActive(false);
        }
    }

    private void GameOver()
    {
        if (!GameManager.Instance.IsGameOver && transform.position.z <= 2f)
        {
            UnloadResources();

            GameManager.Instance.GameOver();
        }
    }

    public void GetHit()
    {
        Life--;

        if (Life == 0) controller.Fission();
    }

    public void GetForce(int particlesCount, int index)
    {
        rb.AddForce(new Vector3(-particlesCount + 1 + index * 2, 0f, 1f) * 7f, ForceMode.Impulse);
    }
}
