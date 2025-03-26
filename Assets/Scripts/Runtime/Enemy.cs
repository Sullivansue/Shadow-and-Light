using MoreMountains.Feedbacks;
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
        public EnemyChaseState  ChaseState { get; set; }
        public EnemyAttackState  AttackState { get; set; }
        public EnemyHittedState  HittedState { get; set; }
        public EnemyDeadState   DeadState { get; set; }
        #endregion
        
        #region 怪物等待
        [field: SerializeField]public float detectionRadius = 1.5f;
        [HideInInspector]public int isFirstTimeIdle = 0;
        #endregion

        #region 怪物受击
        public bool isHitted {get; set;}
        public bool isFinishedHit {get; set;}
        public int attackCount {get; set;}
        public bool isHitRightThere {get; set;}
        [field: SerializeField]public AnimationCurve hitBackCurve;
        [field: SerializeField]public float hitBackDuration;
        #endregion
        
        
        #region 怪物攻击
        public int attackStage { get; set; }
        [field:SerializeField]public float chaseSpeed { get; set; }
        #endregion

        #region 反馈效果

        [field: SerializeField] public MMF_Player hitPlayer;
        [field: SerializeField] public MMF_Player deathPlayer;
        

        #endregion

        public GameObject player {get; set;}
        public float distance {get; set;}
        

        private void Awake()
        {
            stateMachine = new EnemyStateMachine();
            IdleState = new EnemyIdleState(this, stateMachine);
            WaitState = new EnemyWaitState(this, stateMachine);
            ChaseState = new EnemyChaseState(this, stateMachine);
            AttackState = new EnemyAttackState(this, stateMachine);
            HittedState = new EnemyHittedState(this, stateMachine);
            DeadState = new EnemyDeadState(this, stateMachine);
        }
        
        private void Start()
        {
            stateMachine.Initialize(WaitState);
            player = GameObject.Find("Player");
        }

        private void Update()
        {
            distance = Vector3.Distance(player.transform.position, transform.position);
            stateMachine.currentState.UpdateState();
        }
    }
}