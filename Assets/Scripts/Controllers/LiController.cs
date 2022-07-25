using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using Spawners;
using Tool;

namespace Controllers
{
    public class LiController : TargetController
    {
        public override string Name => "TargetLi";

        public override int Mass => 6;

        public override int Score => 3;

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
            yield return AssetLoader.LoadPrefabAsync<GameObject>("Targets/TargetLi", x =>
            {
                resource = Object.Instantiate(x);
            });
        }
    }
}