using System.Collections;
using UnityEngine;

namespace Runtime
{
    public class CollectableParticle : MonoBehaviour
    {
        [Header("射线设置")]
        [SerializeField] protected float checkInterval = 0.1f; // 检测间隔
        [SerializeField] protected float pickupDistance = 2f;  // 拾取距离
        [SerializeField] protected LayerMask playerLayer;      // 玩家层级
        [SerializeField] protected int raysPerFrame = 8;      // 每帧发射射线数量

        private Transform playerTransform;
        protected Transform targetTransform;
        private Bar bar;
        private bool isFirstAdd = true;
        private Player _player;

        [SerializeField]private int particleAmount;

        public virtual void Start()
        {
            _player = GameObject.Find("Player").GetComponent<Player>();
            playerTransform = GameObject.Find("Player").transform;
            targetTransform = playerTransform;
            bar = GameObject.FindGameObjectWithTag("PlayerPower").GetComponent<Bar>();
        }

        public virtual void Update()
        {
            
            CheckPlayerInRange();

            // 保持原有的漂浮和旋转逻辑
            UpdateFloatAnimation();
        }

        protected void CheckPlayerInRange()
        {
            Vector3 toPlayer = targetTransform.position - transform.position;
            float distance = toPlayer.magnitude;

            if (distance > pickupDistance) return;

            // 多方向射线检测
            for (int i = 0; i < raysPerFrame; i++)
            {
                Vector3 rayDir = Quaternion.Euler(
                    Random.Range(-30f, 30f),
                    Random.Range(-30f, 30f),
                    0
                ) * toPlayer.normalized;

                Ray ray = new Ray(transform.position, rayDir);
                
                if (Physics.Raycast(ray, out RaycastHit hit, pickupDistance, playerLayer))
                {
                    if (IsValidPlayer(hit.collider))
                    {
                        OnCollected();
                        break;
                    }
                }

                // 调试绘制
                Debug.DrawRay(ray.origin, ray.direction * pickupDistance, Color.yellow, checkInterval);
            }
        }

        protected virtual bool IsValidPlayer(Collider col)
        {
            return col.CompareTag("Player") && 
                   col is CapsuleCollider &&
                   Vector3.Distance(transform.position, col.transform.position) <= pickupDistance;
        }

        private void UpdateFloatAnimation()
        {
            // 保持原有的漂浮和旋转动画逻辑
            // [原有代码保持不变]
        }

        protected virtual void OnCollected()
        {
            Debug.Log("粒子被射线拾取！");
            // 执行收集逻辑
            if (isFirstAdd && _player.gatherTotalValue <= 100)
            {
                bar.Change(particleAmount);
                _player.gatherTotalValue += particleAmount;
                isFirstAdd = false;
            }
            
            Destroy(gameObject);
            //StartCoroutine(DestroyAfterBarChange());
        }

        /*IEnumerator DestroyAfterBarChange()
        {
            
        }*/
    }
}
