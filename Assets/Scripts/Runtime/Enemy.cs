using UnityEngine;
using Runtime.EnemyState;

namespace Runtime
{
    public class Enemy :  MonoBehaviour
    {
        #region 怪物状态
        public EnemyStateMachine stateMachine { get; set; }
        public EnemyIdleState  IdleState { get; set; }
        public EnemyWaitState  WaitState { get; set; }
        public EnemyWalkState  RunState { get; set; }
        public EnemyAttackState  AttackState { get; set; }
        public EnemyHittedState  HittedState { get; set; }
        #endregion
        
        #region 怪物等待
        [field: SerializeField]public float detectionRadius = 1.5f;
        public int isFirstTimeIdle = 0;
        #endregion

        #region 怪物受击
        public bool isHitted = false;
        public bool isHitRightThere = false;
        #endregion
        

        private void Awake()
        {
            stateMachine = new EnemyStateMachine();
            IdleState = new EnemyIdleState(this, stateMachine);
            WaitState = new EnemyWaitState(this, stateMachine);
            RunState = new EnemyWalkState(this, stateMachine);
            AttackState = new EnemyAttackState(this, stateMachine);
            HittedState = new EnemyHittedState(this, stateMachine);
        }
        
        private void Start()
        {
            stateMachine.Initialize(WaitState);
        }

        private void Update()
        {
            stateMachine.currentState.UpdateState();
        }
    }
}