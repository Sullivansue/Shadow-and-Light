using System.Collections;
using UnityEngine;
using DG.Tweening;
using HighlightPlus;

namespace Runtime.PlayerState
{
    public class PlayerIdleState : State
    {
        private Animator anim;
        private GameObject rotateObject;
        private HighlightTrigger mouseTrigger;
        
        public PlayerIdleState(Player player, StateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void EnterState()
        {
            rotateObject = GameObject.Find("Player").transform.GetChild(0).gameObject;
            anim = rotateObject.GetComponent<Animator>();
            if (!_player.isHoldingSword)
            {
                anim.CrossFade("Idle", 0.1f);
            }
            else
            {
                anim.Play("SwordIdle");
            }
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
                
                anim.Play("SheathBack");
                //_player.swordPrefab.SetActive(false);
            }

            // 人物攻击
            if (Input.GetMouseButtonDown(0) && _player.isHoldingSword 
                                            && _player.target != null)
            {
                _player.stateMachine.ChangeState(_player.AttackState);
            }

            // 人物受击
            if (_player.isHit && _player.isHitRightThere)
            {
                _player.stateMachine.ChangeState(_player.HittedState);
            }
            
            // 人物法阵
            if (Input.GetKey(KeyCode.Space) && _player.isHoldingSword
                && _player.gatherTotalValue > 0)
            {
                _player.stateMachine.ChangeState(_player.GatherState);
            }
            
            // 人物蓄力斩
            if (_player.isHoldingSword && Input.GetKey(KeyCode.Z))
            {
                _player.stateMachine.ChangeState(_player.ChargingState);
            }
        }


        public override void ExitState()
        {
            _player.isFinishedSheathBack = false;
        }
    }
}