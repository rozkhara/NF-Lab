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

    private Vector3 force;

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
        GetStuck();
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
        // 타겟끼리 충돌할 때
        if (collision.gameObject.tag == "Target")
        {
            GetHit();

            TargetSpawner.targetPool[controller.Name].Enqueue(controller);
            gameObject.SetActive(false);
        }

        // 위아래 벽에 충돌할 때
        else if (collision.gameObject.tag == "VerticalCurtain")
        {
            force.y = -force.y;
            rb.AddForce(force, ForceMode.Impulse);
        }

        // 좌우 벽에 충돌할 때
        else if (collision.gameObject.tag == "HorizontalCurtain")
        {
            force.x = -force.x;
            rb.AddForce(force, ForceMode.Impulse);
        }
    }

    private static void UnloadResources()
    {
        allTarget.Clear();
    }

    private void Move()
    {
        if (!controller.IsParticle) transform.Translate(Vector3.back * 1f * Time.deltaTime);
    }

    private void GetStuck()
    {
        var pos = transform.position;

        if (pos.z > 20f)
        {
            pos.z = 20f;
            transform.position = pos;

            rb.velocity = Vector3.zero;

            controller.IsParticle = false;
        }
    }

    private void Disappear()
    {
        var pos = transform.position;

        if (pos.x < -7f || pos.x > 7f || pos.y < 0f || pos.y > 13f)
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
        if (controller.IsParticle) return;

        Life--;

        if (Life == 0) controller.Fission();
    }

    public void GetForce(Vector3 direction)
    {
        force = direction * 7f;

        rb.AddForce(force, ForceMode.Impulse);
    }
}
