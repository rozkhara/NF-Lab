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

                Holder = go.AddComponent<Target>();
                Holder.Controller = new HeController();

                targets.Enqueue(Holder.Controller);
            }
        }
    }
}
