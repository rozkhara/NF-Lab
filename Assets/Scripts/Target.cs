using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Target : MonoBehaviour
{
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

    private void Update()
    {
        if (!(controller is { IsResourceLoaded: true })) return;

        Move();

        controller.OnUpdate();
    }

    private void OnDestroy()
    {
        Controller = null;
    }

    private void Move()
    {
        transform.Translate(Vector3.back * 1f * Time.deltaTime);
    }

    public void GetHit()
    {
        Life--;

        if (Life == 0) controller.Fission();
    }
}
