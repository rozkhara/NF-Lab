using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Guns
{
    public class HandGunController : GunController
    {
        public override float FiringRate => 1f;

        public override int ReloadBulletCount => 9;

        protected override void OnAttached()
        {
            
        }

        protected override void OnDetached()
        {

        }
    }
}
