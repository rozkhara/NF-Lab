using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using Tool;

namespace Generators
{
    public class HeGenerator : TargetGenerator
    {
        public override string Name => "TargetHe";

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

                go.tag = "Target";

                var rb = go.AddComponent<Rigidbody>();
                rb.useGravity = false;
                rb.freezeRotation = true;

                var target = go.AddComponent<Target>();
                target.Controller = new HeController();

                targets.Enqueue(target.Controller);
            }
        }
    }
}
