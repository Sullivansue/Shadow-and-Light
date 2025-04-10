using UnityEngine;
using System.Collections;
using MoreMountains.Feedbacks;

namespace Runtime.PlayerState
{
    public class PlayerHittedState : State
    {
        private GameObject playerPrefab;
        private GameObject enemyPrefab;
        private MMF_Player hitPlayer;
        private Animator animator;

        private Bar bar;
        
        public PlayerHittedState(Player player, StateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void EnterState()
        {
            bar = GameObject.FindGameObjectWithTag("PlayerBlood").GetComponent<Bar>();
            bar.Change(-_player.hitCount);
            _player.totalHP -= _player.hitCount;
            
            
            playerPrefab = _player.gameObject;
            enemyPrefab = _player.hitByWho;
            hitPlayer = _player.hitPlayer;

            animator = playerPrefab.GetComponentInChildren<Animator>();
            animator.Play("Player_Hit");

            hitPlayer.PlayFeedbacks();
            //_player.StartCoroutine(PlayerHittedAnim());
        }

        IEnumerator PlayerHittedAnim()
        {
            float timer = 0f;
        
            // 击退位移方向变化曲线
            Vector3 dir = (enemyPrefab.transform.position - playerPrefab.transform.position).normalized;
            Vector3 originPos = playerPrefab.transform.position;

            hitPlayer.PlayFeedbacks();
        
            while (timer < _player.hitBackDuration)
            {
                timer += Time.deltaTime;
                float progress = timer / _player.hitBackDuration;
                float curveValue = _player.hitBackCurve.Evaluate(progress);
        
                // 更新位置
                playerPrefab.transform.position = originPos + new Vector3(
                    curveValue * dir.x,
                    0f,
                    curveValue * dir.z
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
            /*if (_player.totalHP <= 0)
            {
                _player.stateMachine.ChangeState(_player.DeadState);
            }*/
        }

        public override void ExitState()
        {
            hitPlayer.StopAllCoroutines();
            _player.isHit = false;
            _player.isHitRightThere = false;
            _player.isHitBySpike = false;
        }
    }
}