using System;
using UnityEngine;

namespace Runtime
{
    public class Shield : MonoBehaviour
    {
        private Transform target;
        private Enemy _enemy;
        private Bar bar;
        
        public bool isHit { get; set; }
        public bool isHitRightThere { get; set; }

        private void Start()
        {
            _enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
            target = _enemy.transform;
            bar = GameObject.Find("Canvas").transform.GetChild(3).GetComponent<Bar>();
        }

        private void Update()
        {
            if (_enemy.gatherValue <= 0)
            {
                _enemy.isShieldBroken = true;
                Destroy(this.gameObject);
            }
            else
            {
                transform.position = target.position;
                if (isHit)
                {
                    _enemy.gatherValue -= 100;
                    bar.Change(-100);
                    isHit = false;
                    isHitRightThere = false;
                    _enemy.startAttack = true;
                }
            }
            
        }
    }
}