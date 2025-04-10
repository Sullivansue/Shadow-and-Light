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
        private EnemyRaycastHit raycastHit;
        private MMF_Player spikePlayer;

        private void Start()
        {
            _enemy = transform.parent.gameObject.GetComponent<Enemy>();
            _player = GameObject.Find("Player").GetComponent<Player>();
            actualEnemy = _enemy.transform.GetChild(0).gameObject;
            animator = GetComponent<Animator>();
            deathPlayer = _enemy.deathPlayer;
            raycastHit = _enemy.gameObject.GetComponentInChildren<EnemyRaycastHit>();
            spikePlayer = _enemy.spikePlayer;
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
            //_player.isHitRightThere = true;
            raycastHit.CheckForHit();
        }

        public void FinishedAttackOne()
        {
            if (_enemy.totalHP > 0)
            {
                _enemy.stateMachine.ChangeState(_enemy.AttackState);
            }
            
        }

        public void FinishedDeath()
        {
            StartCoroutine(FeedbackControl());
            
        }

        IEnumerator FeedbackControl()
        {
            deathPlayer.PlayFeedbacks();
            _enemy.transform.GetChild(0).gameObject.SetActive(false);
            yield return new WaitForSeconds(1.5f);
            Destroy(_enemy.gameObject);
            
            
        }

        public void FinishedBurn()
        {
            _enemy.isFinishedBurn = true;
            _enemy.stateMachine.ChangeState(_enemy.AttackState);
        }

        public void SpikeHitGround()
        {
            spikePlayer.PlayFeedbacks();
        }
        
        public void FinishedSpike()
        {
            _enemy.isFinishedSpike = true;
            
        }
    }
}