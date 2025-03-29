using MoreMountains.Feedbacks;
using UnityEngine;

namespace Runtime.PlayerState
{
    public class PlayerChargingAttackState : State
    {
        private Animator anim;
        private MMF_Player chargingPlayer;
        private ParticleSystemManager particleManager;
        public PlayerChargingAttackState(Player player, StateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void EnterState()
        {
            particleManager = GameObject.Find("ParticleManager").GetComponent<ParticleSystemManager>();
            chargingPlayer = _player.chargingPlayer;
            anim = _player.transform.GetChild(0).gameObject.GetComponent<Animator>();
            anim.Play("SwordImpact");
            chargingPlayer.PlayFeedbacks();
        }

        public override void UpdateState()
        {
            if (!chargingPlayer.IsPlaying)
            {
                _player.stateMachine.ChangeState(_player.IdleState);
            }
        }
    }
}