using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using Generators;

namespace Spawners
{
    public class TargetSpawner : MonoBehaviour
    {
        public static Dictionary<string, Queue<TargetController>> targetPool = new Dictionary<string, Queue<TargetController>>();

        private readonly Dictionary<int, string> targets = new Dictionary<int, string>();

        private List<TargetGenerator> targetGenerators = new List<TargetGenerator>();

        private List<Vector3> spawnPoints = new List<Vector3>();

        private float spawnCounter = 2f;

        private int idx = 0;

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

            // 새로운 타겟이 생길 때마다 추가해 줘야 함
            targets.Add(0, "TargetH");
            targets.Add(1, "TargetHe");
            targets.Add(2, "TargetLi");
            targets.Add(3, "TargetBe");
            targets.Add(4, "TargetB");
            targets.Add(5, "TargetC");
            targets.Add(6, "TargetN");
            targets.Add(7, "TargetO");

            var go = new GameObject();

            foreach (var targetGenerator in targetGenerators)
            {
                targetGenerator.Pool = go;
                targetGenerator.CreateTarget(10);
                targetPool.Add(targetGenerator.Name, targetGenerator.targets);
            }

            // 타겟 생성 위치 설정
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    spawnPoints.Add(new Vector3(i, j, 20f));
                }
            }
        }
        private void Update()
        {
            if (spawnCounter > 0f)
            {
                spawnCounter -= Time.deltaTime;
                return;
            }

            string ranName = targets[Random.Range(0, targets.Count)];

            var target = targetPool[ranName].Dequeue();

            if (idx >= targetPool.Count) idx = 0;

            target.Holder.gameObject.SetActive(true);
            target.Holder.transform.position = spawnPoints[Random.Range(0, 25)];

            spawnCounter = 2f;
        }
    }
}
