using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime
{
    public class SwordParticleHit : MonoBehaviour
    {
        private Enemy _enemy;
        public ParticleSystem ps;

        private void Start()
        {
            _enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
            //ps = _enemy.swordParticle;
        }

        private void OnParticleCollision(GameObject other)
        {
            Debug.Log("碰撞了");
            /*// 获取触发粒子的列表
            List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
            int numParticles = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

            // 遍历所有触发粒子
            for (int i = 0; i < numParticles; i++)
            {
                _enemy.isHitted = true;
                _enemy.isHitRightThere = true;
            }

            // 更新粒子状态（避免重复触发）
            ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);*/
            if (other.CompareTag("Enemy"))
            {
                _enemy.isHitted = true;
                _enemy.isHitRightThere = true;
            }
            
        }
    }
}