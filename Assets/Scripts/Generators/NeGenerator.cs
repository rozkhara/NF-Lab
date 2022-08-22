using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using Tool;

namespace Generators
{
    public class NeGenerator : TargetGenerator
    {
        public override Elements Type => Elements.Ne;

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
                rb.drag = dragValue;

                var target = go.AddComponent<Target>();
                target.Controller = new NeController();

                targets.Enqueue(target.Controller);
            }
        }
    }
}
