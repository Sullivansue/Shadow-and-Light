using System;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Runtime
{
    public class PlayerDamage : MonoBehaviour
    {
        private Player _player;

        private void Start()
        {
            _player = GameObject.Find("Player").GetComponent<Player>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("ShieldHit") && _player.totalHP > 0)
            {
                Debug.Log("撞上了");
                _player.isHitBySpike = true;
            }

            /*
            if (other.CompareTag("EnemyHit"))
            {
                _player.stateMachine.ChangeState(_player.HittedState);
            }*/
        }
    }
}