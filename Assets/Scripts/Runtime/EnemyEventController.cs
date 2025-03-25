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
        private MMF_Player deathPlayer;

        private void Start()
        {
            _enemy = transform.parent.gameObject.GetComponent<Enemy>();
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
            _enemy.stateMachine.ChangeState(_enemy.IdleState);
        }

        public void FinishedDeath()
        {
            //StartCoroutine(DeathAfterDestroy());
            deathPlayer.PlayFeedbacks();
        }

        IEnumerator DeathAfterDestroy()
        {
            deathPlayer.PlayFeedbacks();

            yield return new WaitForSeconds(1.5f);
            
            Destroy(_enemy.gameObject);
        }
    }
}