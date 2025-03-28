using UnityEngine;

namespace Runtime
{
    public class MagicCircleRaycast : CollectableParticle
    {
        private Transform enemyTransform;
        private Enemy _enemy;

        public override void Start()
        {
            enemyTransform = GameObject.Find("Enemy").transform;
            _enemy = enemyTransform.gameObject.GetComponent<Enemy>();
        }

        public override void Update()
        {
            CheckPlayerInRange();
        }


        protected override bool IsValidPlayer(Collider col)
        {
            return col.CompareTag("Enemy") && 
                   col is BoxCollider &&
                   Vector3.Distance(transform.position, col.transform.position) <= pickupDistance;
        }

        protected override void OnCollected()
        {
            Debug.Log("踩到法阵里了");
            
            
            
        }
    }
}