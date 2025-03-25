using System;
using UnityEngine;

namespace Runtime
{
    public class EnemyDamage : MonoBehaviour
    {
        private Enemy _enemy;
        private Player _player;
        private Animator anim;

        private void Start()
        {
            _player = GameObject.Find("Player").GetComponent<Player>();
            _enemy = GetComponent<Enemy>();
            anim = transform.GetChild(0).gameObject.GetComponent<Animator>();
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