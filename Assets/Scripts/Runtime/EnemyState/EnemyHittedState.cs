using UnityEngine;

namespace Runtime.EnemyState
{
    public class EnemyHittedState : EenemyState
    {
        private Animator anim;
        public EnemyHittedState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
        {
        }


        public override void EnterState()
        {
            anim = _enemy.transform.GetChild(0).gameObject.GetComponent<Animator>();
            anim.Play("Anim_Hit");
        }
    }
}