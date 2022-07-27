using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spawners;

public abstract class TargetGenerator
{
    public Queue<TargetController> targets { get; } = new Queue<TargetController>();

    public GameObject Pool { get; set; }

    public abstract string Name { get; }

    protected float dragValue = 0.2f;

    public abstract void CreateTarget(int count);
}
