using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using Spawners;
using Tool;

namespace Controllers
{
    public class NiController : TargetController
    {
        public override Elements Type => Elements.Ni;

        public override int Mass => 28 * 2;

        public override int Score => 28;

        public override int Group => 10;

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
