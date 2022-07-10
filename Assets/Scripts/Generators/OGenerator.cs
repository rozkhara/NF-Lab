using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using Tool;

namespace Generators
{
    public class OGenerator : TargetGenerator
    {
        public override void CreateTarget(int count)
        {
            var go = new GameObject
            {
                transform =
                {
                    parent = Pool.transform,
                }
            };

            Holder = go.AddComponent<Target>();
            Holder.Controller = new OController();

            for (int i = 0; i < count; i++)
            {
                Holder.StartCoroutine(Load());

                targets.Enqueue(Holder.Controller);
            }
        }

        protected override IEnumerator LoadResources()
        {
            yield return AssetLoader.LoadPrefabAsync<GameObject>("Targets/TargetO", x =>
            {
                resource = Object.Instantiate(x);
            });
        }
    }
}
