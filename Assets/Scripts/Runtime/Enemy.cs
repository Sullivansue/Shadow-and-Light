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
        public EnemyBurnState   BurnState { get; set; }
        public EnemySpikeState SpikeState { get; set; }
        #endregion
        
        #region 怪物等待
        [field: SerializeField]public float detectionRadius = 1.5f;
        [HideInInspector]public int isFirstTimeIdle = 0;
        public bool startAttack { get; set; }
        #endregion

        #region 怪物受击
        public bool isHitted {get; set;}   // 普攻
        [Header("普攻数值")]
        [field: SerializeField] public int hitCount;
        [Header("灼烧数值")]
        [field: SerializeField] public int burnCount;
        [Header("总血量")]
        [field: SerializeField] public int totalHP;
        [Header("总蓝量")]
        [field: SerializeField] public int gatherValue; // 满了可以重新召唤防护罩
        public bool isChargingHit {get; set;}   // 蓄力斩
        public bool isChargingActualHit {get; set;}
        public bool isFinishedHit {get; set;}
        public bool isFinishedBurn {get; set;}
        public bool isHitRightThere {get; set;}
        public bool isInCircle {get; set;}
        [field: SerializeField]public AnimationCurve hitBackCurve;
        [field: SerializeField]public float hitBackDuration;
        [field: SerializeField]public ParticleSystem swordParticle;
        #endregion
        
        
        #region 怪物攻击
        public int attackStage { get; set; }
        public bool isFinishedSpike { get; set; }
        [field:SerializeField]public float chaseSpeed { get; set; }
        [field:SerializeField]public GameObject shield { get; set; }
        #endregion

        #region 反馈效果

        [field: SerializeField] public MMF_Player hitPlayer;
        [field: SerializeField] public MMF_Player deathPlayer;
        [field: SerializeField] public MMF_Player spikePlayer;
        

        #endregion

        public GameObject player {get; set;}
        public Player _player {get; set;}
        public float distance {get; set;}
        public bool isShieldBroken {get; set;}
        

        private void Awake()
        {
            stateMachine = new EnemyStateMachine();
            IdleState = new EnemyIdleState(this, stateMachine);
            WaitState = new EnemyWaitState(this, stateMachine);
            ChaseState = new EnemyChaseState(this, stateMachine);
            AttackState = new EnemyAttackState(this, stateMachine);
            HittedState = new EnemyHittedState(this, stateMachine);
            DeadState = new EnemyDeadState(this, stateMachine);
            BurnState = new EnemyBurnState(this, stateMachine);
            SpikeState = new EnemySpikeState(this, stateMachine);
        }
        
        private void Start()
        {
            stateMachine.Initialize(WaitState);
            player = GameObject.Find("Player");
            _player = player.gameObject.GetComponent<Player>();
        }

        private void Update()
        {
            if (totalHP <= 0)
            {
                stateMachine.ChangeState(DeadState);
            }
            
            if (isShieldBroken && gatherValue >= 1000)
            {
                Instantiate(shield, transform.position, Quaternion.identity);
                totalHP += 200;
                isShieldBroken = false;
            }
            
            distance = Vector3.Distance(player.transform.position, transform.position);
            stateMachine.currentState.UpdateState();
        }
    }
}