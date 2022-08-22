using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using Spawners;
using Tool;

namespace Controllers
{
    public class KrController : TargetController
    {
        public override Elements Type => Elements.Kr;

        public override int Mass => 36 * 2;

        public override int Score => 36;

        public override int Group => 18;

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
