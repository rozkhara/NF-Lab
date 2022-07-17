using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tool;

namespace Guns
{
    public class HandGunController : GunController
    {
        public override void OnUpdate()
        {

        }

        protected override void OnAttached()
        {
            Holder.FiringRate = 0.3f;
        }

        protected override void OnDetached()
        {

        }

        protected override IEnumerator LoadResources()
        {
            yield return AssetLoader.LoadPrefabAsync<GameObject>("Guns/HandGun", x =>
            {
                resource = Object.Instantiate(x);
            });
        }
    }
}
