using UnityEngine;
using DG.Tweening;
using MoreMountains.Feedbacks;

namespace Runtime.EnemyState
{
    public class EnemyChaseState : EenemyState
    {
        private GameObject playerPrefab;
        private GameObject enemyPrefab;
        private Animator animator;
        private Player _player;
        public EnemyChaseState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
        {
        }

        public override void EnterState()
        {
            playerPrefab = GameObject.Find("Player");
            _player = playerPrefab.GetComponent<Player>();
            enemyPrefab = _enemy.gameObject;
            animator = _enemy.transform.GetChild(0).GetComponent<Animator>();
        }

        public override void UpdateState()
        {
            // 面向玩家
            Vector3 dir = _enemy.transform.position - playerPrefab.transform.position;
            dir.Normalize();
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            enemyPrefab.transform.DORotate(targetRotation.eulerAngles, 0.2f)
                .SetEase(Ease.OutQuad);
            
            enemyPrefab.transform.position += -dir * _enemy.chaseSpeed * Time.deltaTime;
            
            animator.Play("Walk");


            if (_enemy.isInCircle)
            {
                _enemy.stateMachine.ChangeState(_enemy.BurnState);
            }
                
            if (_enemy.distance < 15f)
            {
                _enemy.stateMachine.ChangeState(_enemy.AttackState);
            }

            if (_player.gatherTotalValue < 30)
            {
                _enemy.stateMachine.ChangeState(_enemy.IdleState);
            }
            
            
        }
    }
}