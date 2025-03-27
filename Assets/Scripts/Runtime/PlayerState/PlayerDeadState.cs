using UnityEngine;

namespace Runtime.PlayerState
{
    public class PlayerDeadState : State
    {
        private Animator animator;
        public PlayerDeadState(Player player, StateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void EnterState()
        {
            animator = _player.gameObject.GetComponentInChildren<Animator>();
            animator.Play("Death");
        }
    }
}