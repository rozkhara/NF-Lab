using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spawners
{
    public class ParticleSpawner : MonoBehaviour
    {
        public static readonly Dictionary<string, Queue<ParticleSystem>> particlePool = new Dictionary<string, Queue<ParticleSystem>>();

        private Queue<ParticleSystem> particleQueue = new Queue<ParticleSystem>();

        private void Awake()
        {
            var go = new GameObject("Particles");

            foreach (var particle in ParticleManager.Instance.particles)
            {
                particlePool.Add(particle.name, particleQueue);

                for (int i = 0; i < 30; i++)
                {
                    var p = Instantiate(particle, go.transform);

                    particleQueue.Enqueue(p);
                }
            }
        }
    }
}
