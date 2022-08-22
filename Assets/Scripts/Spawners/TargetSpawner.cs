using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using Generators;

namespace Spawners
{
    public class TargetSpawner : MonoBehaviour
    {
        public static readonly Dictionary<Elements, Queue<TargetController>> targetPool = new Dictionary<Elements, Queue<TargetController>>();

        private readonly Dictionary<int, Elements> targets = new Dictionary<int, Elements>();

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
            targetTypes.Add(new FController());
            targetTypes.Add(new NeController());
            targetTypes.Add(new NaController());
            targetTypes.Add(new MgController());
            targetTypes.Add(new AlController());
            targetTypes.Add(new SiController());
            targetTypes.Add(new PController());
            targetTypes.Add(new SController());
            targetTypes.Add(new ClController());
            targetTypes.Add(new ArController());
            targetTypes.Add(new KController());
            targetTypes.Add(new CaController());
            targetTypes.Add(new ScController());
            targetTypes.Add(new TiController());
            targetTypes.Add(new VController());
            targetTypes.Add(new CrController());
            targetTypes.Add(new MnController());
            targetTypes.Add(new FeController());
            targetTypes.Add(new CoController());
            targetTypes.Add(new NiController());
            targetTypes.Add(new CuController());
            targetTypes.Add(new ZnController());
            targetTypes.Add(new GaController());
            targetTypes.Add(new GeController());
            targetTypes.Add(new AsController());
            targetTypes.Add(new SeController());
            targetTypes.Add(new BrController());
            targetTypes.Add(new KrController());

            // 새로운 타겟 생성기가 생길 때마다 추가해 줘야 함
            targetGenerators.Add(new HGenerator());
            targetGenerators.Add(new HeGenerator());
            targetGenerators.Add(new LiGenerator());
            targetGenerators.Add(new BeGenerator());
            targetGenerators.Add(new BGenerator());
            targetGenerators.Add(new CGenerator());
            targetGenerators.Add(new NGenerator());
            targetGenerators.Add(new OGenerator());
            targetGenerators.Add(new FGenerator());
            targetGenerators.Add(new NeGenerator());
            targetGenerators.Add(new NaGenerator());
            targetGenerators.Add(new MgGenerator());
            targetGenerators.Add(new AlGenerator());
            targetGenerators.Add(new SiGenerator());
            targetGenerators.Add(new PGenerator());
            targetGenerators.Add(new SGenerator());
            targetGenerators.Add(new ClGenerator());
            targetGenerators.Add(new ArGenerator());
            targetGenerators.Add(new KGenerator());
            targetGenerators.Add(new CaGenerator());
            targetGenerators.Add(new ScGenerator());
            targetGenerators.Add(new TiGenerator());
            targetGenerators.Add(new VGenerator());
            targetGenerators.Add(new CrGenerator());
            targetGenerators.Add(new MnGenerator());
            targetGenerators.Add(new FeGenerator());
            targetGenerators.Add(new CoGenerator());
            targetGenerators.Add(new NiGenerator());
            targetGenerators.Add(new CuGenerator());
            targetGenerators.Add(new ZnGenerator());
            targetGenerators.Add(new GaGenerator());
            targetGenerators.Add(new GeGenerator());
            targetGenerators.Add(new AsGenerator());
            targetGenerators.Add(new SeGenerator());
            targetGenerators.Add(new BrGenerator());
            targetGenerators.Add(new KrGenerator());

            // 새로운 타겟이 생길 때마다 추가해 줘야 함
            for (int i = 0; i < 36; i++)
            {
                targets.Add(i, (Elements)i);
            }
            //targets.Add(0, Elements.H);
            //targets.Add(1, Elements.He);
            //targets.Add(2, Elements.Li);
            //targets.Add(3, Elements.Be);
            //targets.Add(4, Elements.B);
            //targets.Add(5, Elements.C);
            //targets.Add(6, Elements.N);
            //targets.Add(7, Elements.O);
            //targets.Add(8, Elements.F);
            //targets.Add(9, Elements.Ne);
            //targets.Add(10, Elements.Na);
            //targets.Add(11, Elements.Mg);
            //targets.Add(12, Elements.Al);
            //targets.Add(13, Elements.Si);
            //targets.Add(14, Elements.P);
            //targets.Add(15, Elements.S);
            //targets.Add(16, Elements.Cl);
            //targets.Add(17, Elements.Ar);
            //targets.Add(18, Elements.K);
            //targets.Add(19, Elements.Ca);
            //targets.Add(20, Elements.Sc);
            //targets.Add(21, Elements.Ti);
            //targets.Add(22, Elements.V);
            //targets.Add(23, Elements.Cr);
            //targets.Add(24, Elements.Mn);
            //targets.Add(25, Elements.Fe);
            //targets.Add(26, Elements.Co);
            //targets.Add(27, Elements.Ni);
            //targets.Add(28, Elements.Cu);
            //targets.Add(29, Elements.Zn);
            //targets.Add(30, Elements.Ga);
            //targets.Add(31, Elements.Ge);
            //targets.Add(32, Elements.As);
            //targets.Add(33, Elements.Se);
            //targets.Add(34, Elements.Br);
            //targets.Add(35, Elements.Kr);
            //targets.Add(36, Elements.Rb);
            //targets.Add(37, Elements.Sr);
            //targets.Add(38, Elements.Y);
            //targets.Add(39, Elements.Zr);
            //targets.Add(40, Elements.Nb);
            //targets.Add(41, Elements.Mo);
            //targets.Add(42, Elements.Tc);
            //targets.Add(43, Elements.Ru);
            //targets.Add(44, Elements.Rh);
            //targets.Add(45, Elements.Pd);
            //targets.Add(46, Elements.Ag);
            //targets.Add(47, Elements.Cd);
            //targets.Add(48, Elements.In);
            //targets.Add(49, Elements.Sn);
            //targets.Add(50, Elements.Sn);
            //targets.Add(51, Elements.Te);
            //targets.Add(52, Elements.I);
            //targets.Add(53, Elements.Xe);

            var go = new GameObject();

            // 오브젝트 풀링
            foreach (var targetGenerator in targetGenerators)
            {
                targetGenerator.Pool = go;
                targetGenerator.CreateTarget(50);
                targetPool.Add(targetGenerator.Type, targetGenerator.targets);
            }

            // 타겟 생성 위치 설정
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    spawnPoints.Add(new Vector3(-2.1f + i * 1.05f, 1 + j * 1.05f, 15f));
                }
            }

            // 정가운데에는 스폰 안되게
            spawnPoints.RemoveAt(12);
        }

        private void Update()
        {
            if (spawnCounter > 0f)
            {
                spawnCounter -= Time.deltaTime;
                return;
            }

            Random.InitState((int)Time.time);

            // 스폰 개수 설정
            int ran = Random.Range(1, 5);

            List<int> spawnPointIdxList = new List<int>();

            for (int i = 0; i < 24; i++)
            {
                spawnPointIdxList.Add(i);
            }

            for (int i = 0; i < ran; i++)
            {
                Elements ranType = targets[Random.Range(0, targets.Count)];

                var target = targetPool[ranType].Dequeue();

                int idx = Random.Range(0, spawnPointIdxList.Count);

                target.Holder.transform.localPosition = spawnPoints[spawnPointIdxList[idx]];
                target.Holder.gameObject.SetActive(true);

                spawnPointIdxList.RemoveAt(idx);
            }

            spawnCounter = 2f;
        }
    }
}
