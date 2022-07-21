using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using Generators;

namespace Spawners
{
    public class TargetSpawner : MonoBehaviour
    {
        public static readonly Dictionary<string, Queue<TargetController>> targetPool = new Dictionary<string, Queue<TargetController>>();

        private readonly Dictionary<int, string> targets = new Dictionary<int, string>();

        public static readonly HashSet<TargetController> targetTypes = new HashSet<TargetController>();

        private List<TargetGenerator> targetGenerators = new List<TargetGenerator>();

        private List<Vector3> spawnPoints = new List<Vector3>();

        private float spawnCounter = 2f;

        private void Awake()
        {
            // 새로운 타겟이 생길 때마다 추가해 줘야 함
            targetTypes.Add(new HController());
            targetTypes.Add(new HeController());
            targetTypes.Add(new LiController());
            targetTypes.Add(new BeController());
            targetTypes.Add(new BController());
            targetTypes.Add(new NController());
            targetTypes.Add(new CController());
            targetTypes.Add(new OController());

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

            // 오브젝트 풀링
            foreach (var targetGenerator in targetGenerators)
            {
                targetGenerator.Pool = go;
                targetGenerator.CreateTarget(500);
                targetPool.Add(targetGenerator.Name, targetGenerator.targets);
            }

            // 타겟 생성 위치 설정
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    spawnPoints.Add(new Vector3(-4 + i * 2, 2 + j * 2, 15f));
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

            // 스폰 개수 설정
            int ran = Random.Range(1, 5);

            List<int> spawnPointIdxList = new List<int>();

            for (int i = 0; i < 25; i++)
            {
                spawnPointIdxList.Add(i);
            }

            for (int i = 0; i < ran; i++)
            {
                string ranName = targets[Random.Range(0, targets.Count)];

                var target = targetPool[ranName].Dequeue();

                int idx = Random.Range(0, spawnPointIdxList.Count);

                target.Holder.transform.localPosition = spawnPoints[spawnPointIdxList[idx]];
                target.Holder.gameObject.SetActive(true);

                spawnPointIdxList.RemoveAt(idx);
            }

            spawnCounter = 2f;
        }
    }
}
