using Runtime.PlayerState;
using UnityEngine;

namespace Runtime
{
    public class Player : MonoBehaviour
    {
        #region 人物状态
        public StateMachine stateMachine { get; set; }
        public PlayerIdleState  IdleState { get; set; }
        public PlayerRunState  RunState { get; set; }
        public PlayerAttackState  AttackState { get; set; }
        #endregion
        
        #region 人物移动
        [field:SerializeField]public AnimationCurve runAccelerationCurve;
        [field:SerializeField]public AnimationCurve runDecelerationCurve;
        [field:SerializeField]public float runAccelerationDuration;
        [field:SerializeField]public float runSpeed;
        [field:SerializeField]public float rotateSpeed;
        #endregion
        
        #region 人物攻击
        public bool isHoldingSword { get; set; }
        [field:SerializeField]public GameObject swordPrefab;
        #endregion

        private void Awake()
        {
            stateMachine = new StateMachine();
            IdleState = new PlayerIdleState(this, stateMachine);
            RunState = new PlayerRunState(this, stateMachine);
            AttackState = new PlayerAttackState(this, stateMachine);
        }

        private void Start()
        {
            stateMachine.Initialize(IdleState);
        }

        private void Update()
        {
            //Debug.Log(stateMachine.currentState);
            stateMachine.currentState.UpdateState();
        }
    }
}