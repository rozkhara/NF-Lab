using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using Spawners;
using Tool;

namespace Controllers
{
    public class MnController : TargetController
    {
        public override Elements Type => Elements.Mn;

        public override int Mass => 25 * 2;

        public override int Score => 25;

        public override int Group => 7;

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
