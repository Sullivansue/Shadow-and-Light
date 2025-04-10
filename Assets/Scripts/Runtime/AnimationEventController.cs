using System;
using UnityEngine;

namespace Runtime
{
    public class AnimationEventController : MonoBehaviour
    {
        public Animator animator;
        public GameObject swordPrefab;
        private Player _player;
        public Enemy _enemy;
        private Shield _shield;

        private void Start()
        {
            _player = GameObject.Find("Player").GetComponent<Player>();
            
        }

        private void Update()
        {
            if (GameObject.FindGameObjectWithTag("Shield"))
            {
                _shield = GameObject.FindGameObjectWithTag("Shield").GetComponent<Shield>();
            }
            
        }

        // 拿出剑结束
        public void SheathOutEndEvent()
        {
            
            animator.CrossFade("SwordIdle", 0.2f);
        }
        
        // 收回剑
        public void SheathBackStartEvent()
        {
            _player.isStartedSheathBack = true;
            _player.isFinishedSheathBack = false;
        }  
        public void SheathBackEndEvent()
        {
            _player.isHoldingSword = false;
            _player.isStartedSheathBack = false;
            _player.isFinishedSheathBack = true;
            animator.CrossFade("Idle", 0.2f);
            swordPrefab.SetActive(false);
        }

        // 攻击结束
        public void AttackFinished()
        {
            _enemy.isHitRightThere = false;
            _enemy.startAttack = false;
            _player.stateMachine.ChangeState(_player.IdleState) ;
        }
        
        // 实际打击
        public void HitRightThere()
        {
            _enemy.isHitRightThere = true;
            _shield.isHitRightThere = true;
        }
        
        // 被打结束
        public void HitFinished()
        {
            _player.stateMachine.ChangeState(_player.IdleState);
            _shield.isHitRightThere = false;
        }
        
        // 聚气结束
        public void GatherFinished()
        {
            _player.stateMachine.ChangeState(_player.IdleState);
        }
        
    }
}