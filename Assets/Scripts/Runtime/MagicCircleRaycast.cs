using System.Collections;
using UnityEngine;

namespace Runtime
{
    public class MagicCircleRaycast : MonoBehaviour
    {
        private Transform enemyTransform;
        private Enemy _enemy;
        public float radius;

        public LayerMask playerLayer;

        void Start()
        {
            enemyTransform = GameObject.Find("Enemy").transform;
            _enemy = enemyTransform.gameObject.GetComponent<Enemy>();
        }

        void Update()
        {
            if (_enemy.stateMachine.currentState != _enemy.BurnState)
            {
                CheckRange();
            }
            
        }
        
        void CheckRange()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, radius, playerLayer);
    
            foreach (var hit in hits)
            {
                if (hit.CompareTag("Enemy"))
                {
                    Debug.Log("石头人进入范围");
                    // 执行逻辑
                    _enemy.isInCircle = true;
                    // 在Scene视图绘制命中射线
                    Debug.DrawLine(transform.position, hit.transform.position, Color.red, 0.1f);
                    StartCoroutine(WaitForTime());
                }
                else
                {
                    _enemy.isInCircle = false;
                }
            }
        }

        IEnumerator WaitForTime()
        {
            yield return new WaitForSeconds(0.1f);
            Destroy(this.gameObject);
        }
        
    }
}