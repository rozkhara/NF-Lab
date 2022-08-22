using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using Spawners;
using Tool;

namespace Controllers
{
    public class VController : TargetController
    {
        public override Elements Type => Elements.V;

        public override int Mass => 23 * 2;

        public override int Score => 23;

        public override int Group => 5;

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
            yield return AssetLoader.LoadPrefabAsync<GameObject>("Targets/poly_" + Group.ToString(), x =>
            {
                resource = Object.Instantiate(x);
                resource.GetComponent<MeshRenderer>().material = CreateMaterials.Instance.CreateMat(Mass);
            });
        }
    }
}
