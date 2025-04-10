using System.Collections;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Runtime.PlayerState
{
    public class PlayerChargingAttackState : State
    {
        private Animator anim;
        private MMF_Player chargingPlayer;
        private ParticleSystemManager particleManager;
        private float timer = 0f;

        private Bar bar;
        
        public PlayerChargingAttackState(Player player, StateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void EnterState()
        {
            bar = GameObject.FindGameObjectWithTag("PlayerPower").GetComponent<Bar>();
            bar.Change(-_player.chargingValue);
            _player.gatherTotalValue -= _player.chargingValue;
            
            particleManager = GameObject.Find("ParticleManager").GetComponent<ParticleSystemManager>();
            chargingPlayer = _player.chargingPlayer;
            anim = _player.transform.GetChild(0).gameObject.GetComponent<Animator>();
            anim.Play("SwordImpact");

            _player.StartCoroutine(ChargingTimeCheck());

        }

        IEnumerator ChargingTimeCheck()
        {
            while (true)
            {
                timer += Time.deltaTime;

                if (timer > 2f)
                {
                    chargingPlayer.PlayFeedbacks();

                    while (chargingPlayer.IsPlaying)
                    {
                        yield return null;
                    }

                    break;

                }
            }

            yield return new WaitForSeconds(1f);
            
            _player.stateMachine.ChangeState(_player.IdleState);
        }


        public override void ExitState()
        {
            timer = 0f;
            chargingPlayer.StopAllCoroutines();
            
            _player.isCharging = false;
        }
    }
}