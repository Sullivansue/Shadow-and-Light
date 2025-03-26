using MoreMountains.Feedbacks;
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
        public PlayerHittedState  HittedState { get; set; }
        #endregion
        
        #region 人物移动
        [field:SerializeField]public AnimationCurve runAccelerationCurve { get; set; }
        [field:SerializeField]public AnimationCurve runDecelerationCurve { get; set; }
        [field:SerializeField]public float runAccelerationDuration { get; set; }
        [field:SerializeField]public float runSpeed { get; set; }
        [field:SerializeField]public float rotateSpeed { get; set; }
        public float originSpeed { get; set; }
        public bool isFinishedSheathBack { get; set; }
        public bool isStartedSheathBack { get; set; }
        #endregion
        
        #region 人物攻击
        public bool isHoldingSword { get; set; }
        public GameObject target { get; set; }
        [field:SerializeField]public GameObject swordPrefab;
        #endregion
        
        #region 人物受击
        [field: SerializeField]public AnimationCurve hitBackCurve { get; set; }
        public bool isHitRightThere { get; set; }
        #endregion

        private void Awake()
        {
            stateMachine = new StateMachine();
            IdleState = new PlayerIdleState(this, stateMachine);
            RunState = new PlayerRunState(this, stateMachine);
            AttackState = new PlayerAttackState(this, stateMachine);
            HittedState = new PlayerHittedState(this, stateMachine);
        }

        private void Start()
        {
            stateMachine.Initialize(IdleState);
            originSpeed = runSpeed;
            isFinishedSheathBack = true;
            
        }

        private void Update()
        {
            //Debug.Log(target);
            stateMachine.currentState.UpdateState();
        }
    }
}