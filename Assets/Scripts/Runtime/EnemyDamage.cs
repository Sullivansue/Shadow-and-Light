using System;
using System.Collections.Generic;
using HighlightPlus;
using UnityEngine;

namespace Runtime
{
    public class EnemyDamage : MonoBehaviour
    {
        private Enemy _enemy;
        private Player _player;
        private Animator anim;
        private HighlightTrigger mouseTrigger;

        private float timerCharging;

        private void Start()
        {
            timerCharging = 0f;
            _player = GameObject.Find("Player").GetComponent<Player>();
            _enemy = GetComponent<Enemy>();
            mouseTrigger = GetComponent<HighlightTrigger>();
            anim = transform.GetChild(0).gameObject.GetComponent<Animator>();
        }

        private void Update()
        {
            float distance = Vector3.Distance(_player.transform.position, _enemy.transform.position);

            //Debug.Log($"isChargingActualHit:{_enemy.isChargingActualHit}");
            if (_player.isCharging && distance <= 41.3f)
            {
                _enemy.isChargingActualHit = true;
                if (_enemy.isFinishedHit && _enemy.stateMachine.currentState == _enemy.HittedState)
                {
                    _enemy.isChargingActualHit = false;
                }
            }
        }

        private void OnMouseEnter()
        {
            _player.target = this.gameObject;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Sword") && 
                _player.stateMachine.currentState == _player.AttackState)
            {
                Debug.Log(other.name);
                _enemy.isHitted = true;
            }
        }

        
        
    }
}