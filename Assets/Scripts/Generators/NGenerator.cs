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

                Holder = go.AddComponent<Target>();
                Holder.Controller = new NController();

                targets.Enqueue(Holder.Controller);
            }
        }
    }
}