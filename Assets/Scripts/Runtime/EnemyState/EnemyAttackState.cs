using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace Runtime.EnemyState
{
    public class EnemyAttackState : EenemyState
    {
        private GameObject playerPrefab;
        private GameObject enemyPrefab;
        private Animator animator;
        public EnemyAttackState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
        {
        }

        public override void EnterState()
        {
            playerPrefab = GameObject.Find("Player");
            enemyPrefab = _enemy.gameObject;
            animator = _enemy.transform.GetChild(0).GetComponent<Animator>();
            // 进入判定是第几阶段
            // 单独写一个追击逻辑
            // 敌人攻击是召唤技能然后近战有攻击
        }

        public override void UpdateState()
        {
            
            /*Debug.Log($"距离为：{_enemy.distance}");
            Debug.Log($"player：{playerPrefab.transform.position}");
            Debug.Log($"enemy：{enemyPrefab.transform.position}");
            Debug.Log($"攻击阶段:{_enemy.attackStage}");*/
            
            // 死了
            if (_enemy.totalHP <= 0)
            {
                _enemy.stateMachine.ChangeState(_enemy.DeadState);
            }
            else
            {
                if (_enemy.distance > 35f)
                {
                    //EnemyChase();
                    _enemy.stateMachine.ChangeState(_enemy.ChaseState);
                    // 后续的远程攻击
                }
                else
                {
                    if (_enemy.attackStage == 0)
                    {
                        _enemy.StartCoroutine(EnemyAttackOne());
                    
                    } 
                }
            }
            
 
            // 被打了强行打断动作
            if (_enemy.isHitted && _enemy.isHitRightThere)
            {
                _enemy.stateMachine.ChangeState(_enemy.HittedState);
            }

            if (_enemy.isChargingHit && _enemy.isChargingActualHit)
            {
                _enemy.stateMachine.ChangeState(_enemy.HittedState);
            }
            
            
        }

        IEnumerator EnemyAttackOne()
        {
            yield return new WaitForSeconds(0.5f);
            animator.Play("Attack1");
            yield break;
        }
    }
}