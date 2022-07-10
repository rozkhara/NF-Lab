using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using Tool;

namespace Generators
{
    public class BeGenerator : TargetGenerator
    {
        public override string Name => "TargetBe";

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
                Holder.Controller = new BeController();

                Holder.StartCoroutine(Load(Holder));

                targets.Enqueue(Holder.Controller);
            }
        }

        protected override IEnumerator LoadResources()
        {
            yield return AssetLoader.LoadPrefabAsync<GameObject>("Targets/TargetBe", x =>
            {
                resource = Object.Instantiate(x);
            });
        }
    }
}
