using System.Collections;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Runtime.EnemyState
{
    public class EnemyHittedState : EenemyState
    {
        private Animator anim;
        private MMF_Player hitPlayer;
        private int count = 0;
        
        public EnemyHittedState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
        {
        }


        public override void EnterState()
        {
            anim = _enemy.transform.GetChild(0).gameObject.GetComponent<Animator>();
            hitPlayer = _enemy.hitPlayer;
            
            anim.Play("Anim_Hit");
            
            hitPlayer.PlayFeedbacks();

            count++;
            
            //
        }

        public override void UpdateState()
        {
            if (count > 4)
            {
                _enemy.stateMachine.ChangeState(_enemy.DeadState);
            }
        }
    }
}