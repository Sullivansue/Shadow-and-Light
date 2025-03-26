using UnityEngine;
using DG.Tweening;

namespace Runtime.EnemyState
{
    public class EnemyChaseState : EenemyState
    {
        private GameObject playerPrefab;
        private GameObject enemyPrefab;
        private Animator animator;
        public EnemyChaseState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
        {
        }

        public override void EnterState()
        {
            playerPrefab = GameObject.Find("Player");
            enemyPrefab = _enemy.gameObject;
            animator = _enemy.transform.GetChild(0).GetComponent<Animator>();
        }

        public override void UpdateState()
        {
            /*float distance = Vector3.Distance(playerPrefab.transform.position, 
                enemyPrefab.transform.position);*/
            
            
            // 面向玩家
            Vector3 dir = _enemy.transform.position - playerPrefab.transform.position;
            dir.Normalize();
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            enemyPrefab.transform.DORotate(targetRotation.eulerAngles, 0.2f)
                .SetEase(Ease.OutQuad);
            
            enemyPrefab.transform.position += -dir * _enemy.chaseSpeed * Time.deltaTime;
            
            animator.Play("Walk");


            if (_enemy.distance < 35f)
            {
                _enemy.stateMachine.ChangeState(_enemy.AttackState);
            }
        }
    }
}