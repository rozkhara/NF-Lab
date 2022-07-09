using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tool;

namespace Guns
{
    public class HandGunController : GunController
    {
        public override float FiringRate => 0.3f;

        public override int ReloadBulletCount => 9;
        
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
            yield return AssetLoader.LoadPrefabAsync<GameObject>("HandGun", x =>
            {
                resource = Object.Instantiate(x);
            });
        }
    }
}
