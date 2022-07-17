using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using Spawners;
using Tool;

namespace Controllers
{
    public class CController : TargetController
    {
        public override void OnUpdate()
        {

        }

        protected override void OnAttached()
        {
            Name = "TargetC";
        }

        protected override void OnDetached()
        {

        }

        protected override IEnumerator LoadResources()
        {
            yield return AssetLoader.LoadPrefabAsync<GameObject>("Targets/TargetC", x =>
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
