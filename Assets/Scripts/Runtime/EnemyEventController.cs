using UnityEngine;

namespace Runtime
{
    public class EnemyEventController :  MonoBehaviour
    {
        public Animator animator;
        public Enemy _enemy;
        

        public void FinishedRise()
        {
            animator.Play("Anim_Idle");
        }
    }
}