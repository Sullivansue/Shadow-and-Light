using MoreMountains.Feedbacks;
using UnityEngine;

namespace Runtime.EnemyState
{
    public class EnemySpikeState : EenemyState
    {
        private Animator anim;
        public EnemySpikeState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
        {
        }

        public override void EnterState()
        {
            anim = _enemy.transform.GetChild(0).GetComponent<Animator>();
            anim.Play("SpikeAttack");
        }

        public override void UpdateState()
        {
            if (_enemy.isFinishedSpike)
            {
                _enemy.stateMachine.ChangeState(_enemy.IdleState);
            }
        }

        public override void ExitState()
        {
            _enemy.isFinishedSpike = false;
        }
    }
}