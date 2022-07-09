using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Guns;

namespace Spawners
{
    public enum GunType
    {
        HandGun,
    }

    public class GunSpawner : MonoBehaviour
    {
        public GunType currentGun;

        private void Awake()
        {
            var go = new GameObject
            {
                transform =
                {
                    parent = transform,
                },
            };

            var gun = go.AddComponent<Gun>();
            gun.Controller = SelectGun(currentGun);
        }

        private GunController SelectGun(GunType gunSelected) => gunSelected switch
        {
            GunType.HandGun => new HandGunController(),
            _ => new HandGunController(),
        };
    }
}
