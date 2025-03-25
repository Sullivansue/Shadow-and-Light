using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace Runtime.PlayerState
{
    public class PlayerIdleState : State
    {
        private Animator anim;
        private GameObject rotateObject;
        
        public PlayerIdleState(Player player, StateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void EnterState()
        {
            rotateObject = GameObject.Find("Player").transform.GetChild(0).gameObject;
            anim = rotateObject.GetComponent<Animator>();
            anim.CrossFade("Idle", 0.1f);
        }

        public override void UpdateState()
        {
            // 基本移动
            bool isBasicInput = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)
                || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S);
            if (isBasicInput)
            {
                _player.stateMachine.ChangeState(_player.RunState);
            }

            // 拿出剑
            if (Input.GetKey(KeyCode.E) && !_player.isHoldingSword)
            {
                _player.isHoldingSword = true;
                _player.swordPrefab.SetActive(true);
                anim.Play("SheathOut");
            }

            // 收剑
            if (Input.GetKey(KeyCode.Q) && _player.isHoldingSword)
            {
                _player.isHoldingSword = false;
                anim.Play("SheathBack");
                //_player.swordPrefab.SetActive(false);
            }

            if (Input.GetMouseButtonDown(0) && _player.isHoldingSword)
            {
                _player.stateMachine.ChangeState(_player.AttackState);
            }
        }
        
        

        
    }
}