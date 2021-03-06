using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using Spawners;
using Tool;

namespace Controllers
{
    public class OController : TargetController
    {
        public override string Name => "TargetO";

        public override int Mass => 16;

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
            yield return AssetLoader.LoadPrefabAsync<GameObject>("Targets/TargetO", x =>
            {
                resource = Object.Instantiate(x);
            });
        }
    }
}
