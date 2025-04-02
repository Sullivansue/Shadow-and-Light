using MoreMountains.Feedbacks;
using UnityEngine;

namespace Runtime.PlayerState
{
    public class PlayerGatherState : State
    {
        private Bar bar;
        private MMF_Player gatherPlayer;
        private Animator animator;
        private ParticleSystem gatherEffect;
        private ParticleSystemManager particleManager;
        public PlayerGatherState(Player player, StateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void EnterState()
        {
            bar = GameObject.Find("Canvas").transform.GetChild(0).GetComponent<Bar>();
            // 生成一次法阵消耗定量蓝
            bar.Change(-_player.gatherValue);
            _player.gatherTotalValue -= _player.gatherValue;

            particleManager = GameObject.Find("ParticleManager").GetComponent<ParticleSystemManager>();
            animator = _player.gameObject.GetComponentInChildren<Animator>();
            gatherPlayer = _player.gatherPlayer;
            animator.Play("SwordPowerUp");
            particleManager.SpawnMagicCircle();
            
            //gatherPlayer.PlayFeedbacks();
        }

        public override void UpdateState()
        {
            bool isBasicInput = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)
                                                        || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S);
            if (isBasicInput)
            {
                _player.stateMachine.ChangeState(_player.RunState);
            }
        }
    }
}