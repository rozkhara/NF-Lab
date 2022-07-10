using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using Spawners;

namespace Controllers
{
    public class NController : TargetController
    {
        public override void OnUpdate()
        {

        }

        protected override void OnAttached()
        {

        }

        protected override void OnDetached()
        {

        }

        public override void Fission()
        {
            Holder.gameObject.SetActive(false);

            string targetName = Holder.transform.GetChild(0).name;

            TargetSpawner.targetPool[targetName.Substring(0, targetName.Length - 7)].Enqueue(this);
        }
    }
}