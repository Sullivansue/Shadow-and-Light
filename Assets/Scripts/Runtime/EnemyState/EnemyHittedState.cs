using System.Collections;
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
        
        public EnemyHittedState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
        {
        }


        public override void EnterState()
        {
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
            if (_enemy.isFinishedHit)
            {
                // 被打15下死
                if (_enemy.attackCount == 15)
                {
                    _enemy.stateMachine.ChangeState(_enemy.DeadState);
                }
            
                //初阶
                if (_enemy.attackCount < 5)
                {
                    _enemy.attackStage = 0;
                    _enemy.stateMachine.ChangeState(_enemy.AttackState);
                }
            
                // 被打5下进入一阶
                if (_enemy.attackCount >= 5 && _enemy.attackCount < 10)
                {
                    _enemy.attackStage = 1;
                    _enemy.stateMachine.ChangeState(_enemy.AttackState);
                }
            
                // 被打10下进入二阶
                if (_enemy.attackCount >= 10 && _enemy.attackCount < 15)
                {
                    _enemy.attackStage = 2;
                    _enemy.stateMachine.ChangeState(_enemy.AttackState);
                }
                
                // 踩到法阵里灼烧
                if (_enemy.isInCircle)
                {
                    // 灼烧逻辑
                }
            }
            
            
        }

        public override void ExitState()
        {
            _enemy.attackCount++;
            _enemy.isFinishedHit = false;
            
        }
    }
}