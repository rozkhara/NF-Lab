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
        controller?.OnUpdate();
    }

    private void OnDestroy()
    {
        Controller = null;
    }

    public void GetHit()
    {
        Life--;

        if (Life == 0) controller.Fission();
    }
}
