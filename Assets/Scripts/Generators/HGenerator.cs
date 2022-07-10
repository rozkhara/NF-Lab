using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using Tool;

namespace Generators
{
    public class HGenerator : TargetGenerator
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
            Holder.Controller = new HController();

            for (int i = 0; i < count; i++)
            {
                Holder.StartCoroutine(Load());

                targets.Enqueue(Holder.Controller);
            }
        }

        protected override IEnumerator LoadResources()
        {
            yield return AssetLoader.LoadPrefabAsync<GameObject>("Targets/TargetH", x =>
            {
                resource = Object.Instantiate(x);
            });
        }
    }
}
