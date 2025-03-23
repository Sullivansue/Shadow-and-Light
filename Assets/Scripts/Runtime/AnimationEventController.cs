using UnityEngine;

namespace Runtime
{
    public class AnimationEventController : MonoBehaviour
    {
        public Animator animator;
        public GameObject swordPrefab;
        
        public void SheathOutEndEvent()
        {
            animator.CrossFade("SwordIdle", 0.2f);
        }
        
        public void SheathBackEndEvent()
        {
            animator.CrossFade("Idle", 0.2f);
            swordPrefab.SetActive(false);
        }
        
    }
}