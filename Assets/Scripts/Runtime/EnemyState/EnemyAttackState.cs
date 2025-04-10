using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace Runtime.EnemyState
{
    public class EnemyAttackState : EenemyState
    {
        private GameObject playerPrefab;
        private Player _player;
        private GameObject enemyPrefab;
        private Animator animator;
        public EnemyAttackState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
        {
        }

        public override void EnterState()
        {
            playerPrefab = GameObject.Find("Player");
            _player = playerPrefab.GetComponent<Player>();
            enemyPrefab = _enemy.gameObject;
            animator = _enemy.transform.GetChild(0).GetComponent<Animator>();
            // 进入判定是第几阶段
            // 单独写一个追击逻辑
            // 敌人攻击是召唤技能然后近战有攻击
            if (_enemy.attackStage == 0 && _enemy.totalHP > 0)
            {
                _enemy.StartCoroutine(EnemyAttackOne());
                    
            } 
        }

        public override void UpdateState()
        {
            if (_enemy.distance > 15f && _player.gatherTotalValue > 20)
            {
                //EnemyChase();
                _enemy.stateMachine.ChangeState(_enemy.ChaseState);
                // 后续的远程攻击
            }
            else if (_player.gatherTotalValue < 20 && _enemy.distance > 15f)
            {
                _enemy.stateMachine.ChangeState(_enemy.IdleState);
            }
                
                
            // 被打了强行打断动作
            if (_enemy.isHitted && _enemy.isHitRightThere)
            {
                _enemy.stateMachine.ChangeState(_enemy.HittedState);
            }
            else if (_enemy.isChargingHit && _enemy.isChargingActualHit)
            {
                _enemy.stateMachine.ChangeState(_enemy.HittedState);
            }

            /*if (_enemy.isInCircle)
            {
                _enemy.stateMachine.ChangeState(_enemy.BurnState);
            }*/
            
        }

        IEnumerator EnemyAttackOne()
        {
            yield return new WaitForSeconds(0.5f);
            animator.Play("Attack1");
            yield break;
        }
    }
}