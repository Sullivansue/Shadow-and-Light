using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime
{
    public class EnemyRaycastHit : MonoBehaviour
    {
        [Header("Raycast Settings")]
        [SerializeField] private Transform[] rayOrigins;  // 手臂上的多个射线发射点
        [SerializeField] private float rayLength = 1.5f; // 射线长度
        [SerializeField] private float rayRadius = 0.3f; // 随机散布半径
        [SerializeField] private int raysPerOrigin = 5;  // 每个发射点的射线数量
        [SerializeField] private LayerMask playerLayer;   // 玩家所在层

        [Header("Debug")]
        [SerializeField] private bool showDebugRays = true;
        [SerializeField] private Color debugRayColor = Color.red;

        private Player _player;

        private void Start()
        {
            _player = GameObject.Find("Player").GetComponent<Player>();
            
        }


        public void CheckForHit()
        {
            bool hasHit = false;
            
            foreach (Transform origin in rayOrigins)
            {
                for (int i = 0; i < raysPerOrigin; i++)
                {
                    Vector3 randomDirection = origin.forward + 
                        Random.insideUnitSphere * rayRadius;
                    
                    Ray ray = new Ray(origin.position, randomDirection.normalized);
                    
                    if (Physics.Raycast(ray, out RaycastHit hit, rayLength, playerLayer))
                    {
                        if (IsValidPlayerHit(hit))
                        {
                            OnPlayerHit(hit);
                            hasHit = true;
                            // 取消注释下面这行如果只需要检测到一次命中
                            //return; 
                        }
                    }
                    
                    // 调试绘制
                    if (showDebugRays)
                    {
                        Debug.DrawRay(ray.origin, ray.direction * rayLength, debugRayColor, 0.5f);
                    }
                }
            }
            
            if (!hasHit)
            {
                Debug.Log("Attack missed!");
            }
        }

        private bool IsValidPlayerHit(RaycastHit hit)
        {
            // 检查是否是胶囊碰撞体且带有玩家标签
            return hit.collider is CapsuleCollider && 
                   hit.collider.CompareTag("Player");
        }

        private void OnPlayerHit(RaycastHit hit)
        {
            Debug.Log("Player hit!");
            _player.isHit = true;
            _player.isHitRightThere = true;
            _player.hitByWho = transform.parent.gameObject;
            
        }
    }
}