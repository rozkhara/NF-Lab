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
        public override string Name => "TargetC";

        public override int Mass => 12;

        public override int Score => 6;

        public override void OnUpdate()
        {

        }

        protected override void OnAttached()
        {

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
    }
}
