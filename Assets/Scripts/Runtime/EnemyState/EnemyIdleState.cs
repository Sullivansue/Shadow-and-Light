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
            anim = _enemy.GetComponent<Animator>();
            anim.Play("Anim_Rise");
        }
    }
}