using UnityEngine;

namespace Runtime.EnemyState
{
    public class EnemyBurnState : EenemyState
    {
        private Animator anim;
        private Bar bar;
        public EnemyBurnState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
        {
        }

        public override void EnterState()
        {
            bar = GameObject.FindGameObjectWithTag("EnemyBlood").GetComponent<Bar>();
            // 灼烧
            bar.Change(-_enemy.burnCount);
            _enemy.totalHP -= _enemy.burnCount;
            
            anim = _enemy.transform.GetChild(0).gameObject.GetComponent<Animator>();
            anim.Play("Anim_Burn");
        }

        public override void UpdateState()
        {
            /*if (_enemy.isFinishedBurn)
            {
                if (_enemy.distance > 15f)
                {
                    _enemy.stateMachine.ChangeState(_enemy.ChaseState);
                }
                else
                {
                    _enemy.stateMachine.ChangeState(_enemy.AttackState);
                }
            }*/
            
        }


        public override void ExitState()
        {
            _enemy.isFinishedBurn = false;
        }
    }
}