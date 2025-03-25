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

        private void Start()
        {
            _player = GameObject.Find("Player").GetComponent<Player>();
        }

        // 拿出剑结束
        public void SheathOutEndEvent()
        {
            animator.CrossFade("SwordIdle", 0.2f);
        }
        
        // 收回剑
        public void SheathBackEndEvent()
        {
            animator.CrossFade("Idle", 0.2f);
            swordPrefab.SetActive(false);
        }

        // 攻击结束
        public void AttackFinished()
        {
            _player.stateMachine.ChangeState(_player.IdleState) ;
        }
        
        // 实际打击
        public void HitRightThere()
        {
            _enemy.isHitRightThere = true;
        }
        
    }
}