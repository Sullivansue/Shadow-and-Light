using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace Runtime.PlayerState
{
    public class PlayerAttackState : State
    {
        private Animator anim;
        private GameObject rotateObject;
        
        public PlayerAttackState(Player player, StateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void EnterState()
        {
            _player.isHoldingSword = true;
            rotateObject = GameObject.Find("Player").transform.GetChild(0).gameObject;
            anim = rotateObject.GetComponent<Animator>();
            anim.Play("SheathOut");
        }

        public override void UpdateState()
        {
            bool isInput = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)
                                                   || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S);
            if (isInput)
            {
                _player.stateMachine.ChangeState(_player.RunState);
            }
        }
        
        

        
    }
}