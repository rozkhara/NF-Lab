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
        var pos = transform.GetChild(0).position;

        if (pos.x < -7f || pos.x > 7f || pos.y < 1f || pos.y > 8f || pos.z > 20f)
        {
            TargetSpawner.targetPool[controller.Name].Enqueue(controller);
            gameObject.SetActive(false);

            Debug.Log("확인!");
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
}
