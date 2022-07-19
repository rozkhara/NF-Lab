using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using Tool;

namespace Generators
{
    public class HGenerator : TargetGenerator
    {
        public override string Name => "TargetH";

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
                target.Controller = new HController();

                targets.Enqueue(target.Controller);
            }
        }
    }
}
