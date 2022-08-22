using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using Spawners;
using Tool;

namespace Controllers
{
    public class CaController : TargetController
    {
        public override Elements Type => Elements.Ca;

        public override int Mass => 20 * 2;

        public override int Score => 20;

        public override int Group => 2;

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
