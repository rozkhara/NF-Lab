using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Target : MonoBehaviour
{
    private static readonly HashSet<Target> allTarget = new HashSet<Target>();

    public int Life { get; set; }

    private TargetController controller;

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
        transform.Translate(Vector3.back * 1f * Time.deltaTime);
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
