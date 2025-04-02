using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Runtime.EnemyState
{
    public class EnemyHittedState : EenemyState
    {
        private Animator anim;
        private MMF_Player hitPlayer;
        private GameObject playerPrefab;
        private GameObject enemyPrefab;

        private Bar bar;
        
        public EnemyHittedState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
        {
        }


        public override void EnterState()
        {
            bar = GameObject.Find("Canvas").transform.GetChild(1).GetComponent<Bar>();
            // 普攻
            bar.Change(-_enemy.hitCount);
            _enemy.totalHP -= _enemy.hitCount;
            
            playerPrefab = GameObject.Find("Player");
            enemyPrefab = _enemy.gameObject;
            anim = _enemy.transform.GetChild(0).gameObject.GetComponent<Animator>();
            hitPlayer = _enemy.hitPlayer;
            
            
            Debug.Log("一直在这里重复");

            //_enemy.StartCoroutine(EnemyHittedAnim());
            
            //
            anim.Play("Anim_Hit");
            hitPlayer.PlayFeedbacks();
        }
        
        IEnumerator EnemyHittedAnim()
        {
            float timer = 0f;
        
            // 击退位移方向变化曲线
            Vector3 dir = (playerPrefab.transform.position - enemyPrefab.transform.position).normalized;
            Vector3 originPos = enemyPrefab.transform.position;

            anim.Play("Anim_Hit");
            hitPlayer.PlayFeedbacks();
        
            while (timer < _enemy.hitBackDuration)
            {
                timer += Time.deltaTime;
                float progress = timer / _enemy.hitBackDuration;
                float curveValue = _enemy.hitBackCurve.Evaluate(progress);
        
                // 更新位置
                enemyPrefab.transform.position = originPos + new Vector3(
                    curveValue * -dir.x,
                    0f,
                    curveValue * -dir.z
                );
                yield return null;
            }
            
            while (hitPlayer.IsPlaying)
            {
                yield return null;
            }

        }
        
        

        public override void UpdateState()
        {
            // 被打15下死
            if (_enemy.totalHP <= 0)
            {
                _enemy.stateMachine.ChangeState(_enemy.DeadState);
            }
            else
            {
                _enemy.stateMachine.ChangeState(_enemy.IdleState);
            }
            
            /*if (_enemy.isFinishedHit)
            {
                
            
                //初阶 光盾
                if (_enemy.totalHP >= 50)
                {
                    _enemy.attackStage = 0;
                    _enemy.stateMachine.ChangeState(_enemy.AttackState);
                }
            
                // 进入二阶
                if (_enemy.totalHP > 0 && _enemy.totalHP < 50)
                {
                    _enemy.attackStage = 1;
                    _enemy.stateMachine.ChangeState(_enemy.AttackState);
                }
                
                // 踩到法阵里灼烧
                if (_enemy.isInCircle && _enemy.totalHP > 0)
                {
                    // 灼烧逻辑
                    _enemy.stateMachine.ChangeState(_enemy.BurnState);
                }
            }*/
            
            
        }

        public override void ExitState()
        {
            Debug.Log(" Exit State");
            _enemy.isFinishedHit = false;
            
            _enemy.isChargingHit = false;
            _enemy.isChargingActualHit = false;
            
        }
    }
}