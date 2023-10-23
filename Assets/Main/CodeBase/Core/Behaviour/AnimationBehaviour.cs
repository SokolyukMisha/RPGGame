using UnityEngine;


namespace Main.CodeBase.Core
{
    public class AnimationBehaviour : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Attack = Animator.StringToHash("Attack");

        public void PlayAttackAnimation()
        {
            animator.SetTrigger(Attack);
        }
        public void UpdateMovementAnimation(float speed)
        {
            animator.SetFloat(Speed, speed);
        }
    }
}