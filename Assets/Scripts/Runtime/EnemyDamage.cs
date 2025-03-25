using System;
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

        private void Start()
        {
            _player = GameObject.Find("Player").GetComponent<Player>();
            _enemy = GetComponent<Enemy>();
            mouseTrigger = GetComponent<HighlightTrigger>();
            anim = transform.GetChild(0).gameObject.GetComponent<Animator>();
        }

        private void Update()
        {
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