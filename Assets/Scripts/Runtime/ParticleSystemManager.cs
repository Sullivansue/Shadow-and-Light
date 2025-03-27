using System;
using UnityEngine;

namespace Runtime
{
    public class ParticleSystemManager : MonoBehaviour
    {
        public ParticleSystem magicCircle;
        private GameObject playerPrefab;

        private void Start()
        {
            playerPrefab = GameObject.Find("Player");
        }

        public void SpawnMagicCircle()
        {
            ParticleSystem newEffect = Instantiate(magicCircle
            , playerPrefab.transform.position, Quaternion.identity);
        }
    }
}