using System;
using System.Collections;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Runtime
{
    public class EnemyEventController :  MonoBehaviour
    {
        private Animator animator;
        private Enemy _enemy;
        private Player _player;
        private MMF_Player deathPlayer;
        private GameObject actualEnemy;

        private void Start()
        {
            _enemy = transform.parent.gameObject.GetComponent<Enemy>();
            _player = GameObject.Find("Player").GetComponent<Player>();
            actualEnemy = _enemy.transform.GetChild(0).gameObject;
            animator = GetComponent<Animator>();
            deathPlayer = _enemy.deathPlayer;
        }

        public void FinishedRise()
        {
            animator.Play("Anim_Idle");
        }

        public void FinishedHitted()
        {
            _enemy.isHitted = false;
            _enemy.isHitRightThere = false;
            _enemy.isFinishedHit = true;
        }

        
        public void AttackRightThere()
        {
            //_enemy.isAttackRightThere = true;
            _player.isHitRightThere = true;
        }

        public void FinishedAttackOne()
        {
            _enemy.stateMachine.ChangeState(_enemy.AttackState);
        }
    }
}