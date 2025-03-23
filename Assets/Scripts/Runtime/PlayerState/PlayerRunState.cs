using System.Collections;
using System.Drawing;
using UnityEngine;
using DG.Tweening;

namespace Runtime.PlayerState
{ 
    public class PlayerRunState : State
    {
        private Animator anim;
        private float pressedTime;
        private GameObject rotateObject;
        private GameObject playerObject;
        
        private AnimationEventController eventController;
        
        public PlayerRunState(Player player, StateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void EnterState()
        {
            rotateObject = GameObject.Find("Player").transform.GetChild(0).gameObject;
            playerObject = GameObject.Find("Player");
            eventController = rotateObject.GetComponent<AnimationEventController>();
            anim = rotateObject.GetComponent<Animator>();

            if (_player.isHoldingSword)
            {
                anim.Play("SwordWalk");
            }
            else
            {
                anim.Play("JogForward");
            }
            
        }

        public override void UpdateState()
        {
            HandleMovement();
            CheckForIdle();
        }
        
        
        private void HandleMovement()
        {
            float horiInput = Input.GetAxisRaw("Horizontal");
            float vertiInput = Input.GetAxisRaw("Vertical");
            
            Vector3 dir = new Vector3(horiInput, 0, vertiInput);
            
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            Vector3 rotate = targetRotation.eulerAngles;
//            Debug.Log(targetRotation);
               
            rotateObject.transform.DORotate(rotate, _player.rotateSpeed)
                .SetEase(Ease.InOutBounce);
                
                
            playerObject.transform.position += 
                dir * _player.runSpeed * Time.deltaTime;
        }
        
        // 是否停止输入
        private void CheckForIdle()
        {
            if (!(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) 
                                          || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
            {
                _player.stateMachine.ChangeState(_player.IdleState);
            }
        }
        
        public override void ExitState()
        {
            rotateObject.transform.DOKill();
        }
    }
}