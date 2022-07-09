using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spawners
{
    public enum GunType
    {
        Handgun,
    }

    public class GunSpawner : MonoBehaviour
    {
        public GunType currentGun;

        private void Awake()
        {
            
        }
    }
}
