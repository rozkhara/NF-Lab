using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using Spawners;
using Tool;

namespace Controllers
{
    public class BeController : TargetController
    {
        public override void OnUpdate()
        {

        }

        protected override void OnAttached()
        {
            Name = "TargetBe";
        }

        protected override void OnDetached()
        {

        }

        protected override IEnumerator LoadResources()
        {
            yield return AssetLoader.LoadPrefabAsync<GameObject>("Targets/TargetBe", x =>
            {
                resource = Object.Instantiate(x);
            });
        }

        public override void Fission()
        {
            Holder.gameObject.SetActive(false);

            TargetSpawner.targetPool[Name].Enqueue(this);
        }
    }
}