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
        public PlayerDeadState  DeadState { get; set; }
        public PlayerGatherState GatherState { get; set; }
        public PlayerChargingAttackState ChargingState { get; set; }
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
        public bool isCharging { get; set; }
        public GameObject target { get; set; }
        [field:SerializeField]public GameObject swordPrefab;
        [field: SerializeField] public MMF_Player gatherPlayer {get; set;}
        [field: SerializeField] public MMF_Player chargingPlayer {get; set;}
        
        #endregion
        
        #region 人物受击
        [field: SerializeField]public AnimationCurve hitBackCurve { get; set; }
        [field: SerializeField]public float hitBackDuration { get; set; }
        [field: SerializeField]public MMF_Player hitPlayer { get; set; }
        //
        //[field: SerializeField]public MMF_Player pushPlayer { get; set; }
        public bool isHitRightThere { get; set; }
        public bool isHit { get; set; }
        public bool isHitBySpike { get; set; }
        public GameObject hitByWho { get; set; }
        
        #endregion
        
        #region 人物数值
        [field:SerializeField] public int gatherValue { get; set; } // 法阵耗蓝
        [field:SerializeField] public int chargingValue { get; set; } // 蓄力耗蓝
        [field:SerializeField] public int gatherTotalValue { get; set; } // 总蓝
        [field: SerializeField]public int hitCount { get; set; } // 受击数值
        [field: SerializeField]public int totalHP { get; set; } // 总血量
        #endregion
        
        public KeyboardInputHandler inputHandler { get; set; }
        public Light light { get; set; }
        public Bar bar { get; set; }

        private void Awake()
        {
            stateMachine = new StateMachine();
            IdleState = new PlayerIdleState(this, stateMachine);
            RunState = new PlayerRunState(this, stateMachine);
            AttackState = new PlayerAttackState(this, stateMachine);
            HittedState = new PlayerHittedState(this, stateMachine);
            DeadState = new PlayerDeadState(this, stateMachine);
            GatherState = new PlayerGatherState(this, stateMachine);
            ChargingState = new PlayerChargingAttackState(this, stateMachine);
        }

        private void Start()
        {
            // 初始蓝条0
            stateMachine.Initialize(IdleState);
            originSpeed = runSpeed;
            isFinishedSheathBack = true;
            inputHandler = GameObject.Find("InputHandler").GetComponent<KeyboardInputHandler>();
            
            /*bar = GameObject.FindGameObjectWithTag("PlayerPower").GetComponent<Bar>();
            bar.Change(-100);
            gatherTotalValue -= 100;*/
            
        }

        private void Update()
        {
            stateMachine.currentState.UpdateState();

            light = GetComponentInChildren<Light>();
            // 计算比例并设置范围
            float ratio = gatherTotalValue / 100f;
            light.range = Mathf.Lerp(0, 20, ratio);

            if (totalHP <= 0)
            {
                stateMachine.ChangeState(DeadState);
            }
        }
    }
}