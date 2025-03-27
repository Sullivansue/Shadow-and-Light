using UnityEngine;

namespace Runtime
{
    [RequireComponent(typeof(SphereCollider), typeof(Rigidbody))]
    public class CollectableParticle : MonoBehaviour
    {
        [Header("射线设置")]
        [SerializeField] private float checkInterval = 0.1f; // 检测间隔
        [SerializeField] private float pickupDistance = 2f;  // 拾取距离
        [SerializeField] private LayerMask playerLayer;      // 玩家层级
        [SerializeField] private int raysPerFrame = 8;      // 每帧发射射线数量

        private Transform playerTransform;
        private float lastCheckTime;

        private void Start()
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            lastCheckTime = Time.time;
        }

        private void Update()
        {
            if (Time.time - lastCheckTime > checkInterval)
            {
                CheckPlayerInRange();
                lastCheckTime = Time.time;
            }

            // 保持原有的漂浮和旋转逻辑
            UpdateFloatAnimation();
        }

        private void CheckPlayerInRange()
        {
            Vector3 toPlayer = playerTransform.position - transform.position;
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

        private bool IsValidPlayer(Collider col)
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

        private void OnCollected()
        {
            Debug.Log("粒子被射线拾取！");
            // 执行收集逻辑
            Destroy(gameObject);
            // 或使用对象池：ParticlePool.Instance.ReturnToPool(gameObject);
        }
        
        // 在粒子周围绘制球形辅助线
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, pickupDistance);
    
            // 绘制主要射线方向
            if (Application.isPlaying && playerTransform != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, 
                    transform.position + (playerTransform.position - transform.position).normalized * pickupDistance);
            }
        }
    }
}
