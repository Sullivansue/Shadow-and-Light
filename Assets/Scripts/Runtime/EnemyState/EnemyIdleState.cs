using Unity.VisualScripting;
using UnityEngine;

namespace Runtime.EnemyState
{
    public class EnemyIdleState : EenemyState
    {
        private Animator anim;
        public EnemyIdleState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
        {
        }

        public override void EnterState()
        {
            anim = _enemy.transform.GetChild(0).GetComponent<Animator>();
            
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
            if (_enemy.isHitted && _enemy.isHitRightThere)
            {
                _enemy.stateMachine.ChangeState(_enemy.HittedState);
            }
        }
    }
}