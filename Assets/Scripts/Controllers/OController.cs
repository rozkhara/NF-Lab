using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;

namespace Controllers
{
    public class OController : TargetController
    {
        private Dictionary<int, List<TargetController>> nextTargets = new Dictionary<int, List<TargetController>>();

        public override void OnUpdate()
        {

        }

        protected override void OnAttached()
        {

        }

        protected override void OnDetached()
        {

        }

        protected override void Fission(Target holder)
        {
            Object.Destroy(holder);

            int atom = Random.Range(2, 5);

            for (int i = 0; i < atom; i++)
            {

            }

            int neutron = Random.Range(1, 4);

            for (int i = 0; i < neutron; i++)
            {

            }
        }
    }
}
