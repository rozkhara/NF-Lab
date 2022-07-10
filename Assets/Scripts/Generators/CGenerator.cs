using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using Tool;

namespace Generators
{
    public class CGenerator : TargetGenerator
    {
        public override string Name => "TargetC";

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

                var target = go.AddComponent<Target>();
                target.Controller = new CController();

                targets.Enqueue(target.Controller);
            }
        }
    }
}
