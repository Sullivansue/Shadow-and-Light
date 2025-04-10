using UnityEngine;

namespace Runtime.EnemyState
{
    public class EnemyWaitState : EenemyState
    {
        private Animator anim;
        private GameObject player;
        public EnemyWaitState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
        {
        }

        public override void EnterState()
        {
            player = GameObject.Find("Player");
            anim = _enemy.transform.GetChild(0).GetComponent<Animator>();
            anim.Play("Anim_Wait");
        }

        public override void UpdateState()
        {
            float dis2Player = Vector3.Distance(_enemy.transform.position, player.transform.position);
//            Debug.Log(dis2Player);
            if (dis2Player < _enemy.detectionRadius)
            {
                _enemy.stateMachine.ChangeState(_enemy.IdleState);
            }

            
        }


        public override void ExitState()
        {
            anim.Play("Anim_Rise");
        }
    }
}