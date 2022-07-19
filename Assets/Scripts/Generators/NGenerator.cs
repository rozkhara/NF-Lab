using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using Tool;

namespace Generators
{
    public class NGenerator : TargetGenerator
    {
        public override string Name => "TargetN";

        public override void CreateTarget(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var go = new GameObject
                {
                    transform =
                    {
                        parent = Pool.transform,
                    }
                };

                var rb = go.AddComponent<Rigidbody>();
                rb.useGravity = false;

                var target = go.AddComponent<Target>();
                target.Controller = new NController();

                targets.Enqueue(target.Controller);
            }
        }
    }
}