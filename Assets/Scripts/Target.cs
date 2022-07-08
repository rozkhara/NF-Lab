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
        if (controller == null) return;

        controller.OnUpdate();
    }

    private void OnDestroy()
    {
        Controller = null;
    }
}
