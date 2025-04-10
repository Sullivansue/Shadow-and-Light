using Unity.VisualScripting;
using UnityEngine;
using HighlightPlus;

namespace Runtime.EnemyState
{
    public class EnemyIdleState : EenemyState
    {
        private Animator anim;
        private HighlightTrigger mouseTrigger;
        private GameObject shield;
        
        public EnemyIdleState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
        {
        }

        public override void EnterState()
        {
            shield = GameObject.FindGameObjectWithTag("Shield");
            anim = _enemy.transform.GetChild(0).GetComponent<Animator>();
            mouseTrigger = _enemy.GetComponent<HighlightTrigger>();
            
            // 不同时机进入不同动画
            if (_enemy.isFirstTimeIdle == 0)
            {
                anim.Play("Anim_Rise");
            }
            else
            {
                anim.Play("Anim_Idle");
            }
            
            // 检查是否第一次进入idle
            if (_enemy.isFirstTimeIdle == 0)
            {
                _enemy.isFirstTimeIdle += 1;
            }
        }

        public override void UpdateState()
        {
            if (_enemy.startAttack && shield != null)
            {
                _enemy.stateMachine.ChangeState(_enemy.SpikeState);
            }
            if (_enemy.isHitted && _enemy.isHitRightThere)
            {
                _enemy.stateMachine.ChangeState(_enemy.HittedState);
            }

            if (_enemy.isChargingHit && _enemy.isChargingActualHit)
            {
                _enemy.stateMachine.ChangeState(_enemy.HittedState);
            }

            if (_enemy.isInCircle)
            {
                _enemy.stateMachine.ChangeState(_enemy.BurnState);
            }
            
        }
    }
}