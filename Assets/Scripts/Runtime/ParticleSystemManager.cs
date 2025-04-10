using System;
using System.Collections;
using UnityEngine;

namespace Runtime
{
    public class ParticleSystemManager : MonoBehaviour
    {
        public GameObject magicCircle;
        public GameObject greatSword;
        public float magidCircleLifetime;
        public float greatSwordLifetime;
        
        
        private GameObject playerPrefab;
        private float timer;
        private GameObject circleEffect;
        private GameObject greatSwordEffect;

        private void Start()
        {
            playerPrefab = GameObject.Find("Player");
            timer = 0f;
        }


        public void SpawnMagicCircle()
        {
            circleEffect = Instantiate(magicCircle
                , playerPrefab.transform.position, Quaternion.identity);
            StartCoroutine(CountParticleTime(magidCircleLifetime, circleEffect));
        }

        IEnumerator CountParticleTime(float seconds, GameObject effect)
        {
            yield return new WaitForSecondsRealtime(seconds);
            Destroy(effect);
            yield return null;
        }

        
    }
}