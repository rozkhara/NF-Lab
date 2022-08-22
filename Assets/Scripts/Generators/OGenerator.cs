using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using Tool;

namespace Generators
{
    public class OGenerator : TargetGenerator
    {
        public override Elements Type => Elements.O;

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
                target.Controller = new OController();

                targets.Enqueue(target.Controller);
            }
        }
    }
}
