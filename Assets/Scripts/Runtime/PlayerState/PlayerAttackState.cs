using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace Runtime.PlayerState
{
    public class PlayerAttackState : State
    {
        private Animator anim;
        private GameObject rotateObject;
        private GameObject target;
        
        public PlayerAttackState(Player player, StateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void EnterState()
        {
            _player.isHoldingSword = true;
            rotateObject = GameObject.Find("Player").transform.GetChild(0).gameObject;
            anim = rotateObject.GetComponent<Animator>();

            _player.StartCoroutine(TurnToTarget());
            
        }

        IEnumerator TurnToTarget()
        {
            // 面向敌人攻击
            Vector3 dir = _player.target.transform.position - _player.transform.position;
            dir.Normalize();
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            rotateObject.transform.DORotate(targetRotation.eulerAngles, 0.2f).SetEase(Ease.OutQuad);
            
            yield return new WaitForSeconds(0.1f);
            
            anim.Play("SwordSlash");
        }

        public override void UpdateState()
        {
            
        }
        
        

        
    }
}