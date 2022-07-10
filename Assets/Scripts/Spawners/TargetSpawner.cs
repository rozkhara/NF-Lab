using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using Generators;

namespace Spawners
{
    public class TargetSpawner : MonoBehaviour
    {
        private List<Queue<TargetController>> targetPool = new List<Queue<TargetController>>();

        private List<TargetGenerator> targetGenerators = new List<TargetGenerator>();

        private void Awake()
        {
            // 새로운 타겟 생성기가 생길 때마다 추가해 줘야 함
            targetGenerators.Add(new HGenerator());
            targetGenerators.Add(new HeGenerator());
            targetGenerators.Add(new LiGenerator());
            targetGenerators.Add(new BeGenerator());
            targetGenerators.Add(new BGenerator());
            targetGenerators.Add(new CGenerator());
            targetGenerators.Add(new NGenerator());
            targetGenerators.Add(new OGenerator());

            var go = new GameObject();

            foreach (var targetGenerator in targetGenerators)
            {
                targetGenerator.Pool = go;
                targetGenerator.CreateTarget(10);
                targetPool.Add(targetGenerator.targets);
            }
        }
    }
}
