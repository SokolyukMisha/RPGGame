using UnityEngine;


namespace Main.CodeBase.Core
{
    public class AnimationBehaviour : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        
        private static readonly int Speed = Animator.StringToHash("Speed");

        public void UpdateMovementAnimation(float speed)
        {
            animator.SetFloat(Speed, speed);
        }
    }
}