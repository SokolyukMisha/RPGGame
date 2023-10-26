using UnityEngine;

namespace Main.CodeBase.Core.Behaviour
{
    [RequireComponent(typeof(Animator))]
    public class AnimationBehaviour : MonoBehaviour
    {
        [Header("Required Components")]
        [SerializeField] private Animator animator;

        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Death = Animator.StringToHash("Death");
        private static readonly int StopAttack = Animator.StringToHash("StopAttack");
        
        private void OnValidate()
        {
            animator ??= GetComponent<Animator>();
        }

        public void PlayAttackAnimation()
        {
            animator.ResetTrigger(StopAttack);
            animator.SetTrigger(Attack);
        }

        public void StopAttackAnimation() =>
            animator.SetTrigger(StopAttack);

        public void UpdateMovementAnimation(float speed) =>
            animator.SetFloat(Speed, speed);
        

        public void PlayDeathAnimation() =>
            animator.SetTrigger(Death);
    }
}