using Main.CodeBase.Core.Behaviour;
using UnityEngine;

namespace Main.CodeBase.Core
{
    [RequireComponent(typeof(AnimationBehaviour))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(ActionScheduler))]
    public class Health : MonoBehaviour
    {
        [SerializeField] private AnimationBehaviour animationBehaviour;
        [SerializeField] private CapsuleCollider capsuleCollider;
        [SerializeField] private ActionScheduler actionScheduler;
        [SerializeField] private float health = 100f;
        public bool IsDead { get; private set; }

        private void OnValidate()
        {
            animationBehaviour ??= GetComponent<AnimationBehaviour>();
            capsuleCollider ??= GetComponent<CapsuleCollider>();
            actionScheduler ??= GetComponent<ActionScheduler>();
        }
        public void TakeDamage(float damage)
        {
            if (IsDead) return;
            
            health -= damage;
            if (health <= 0)
            {
                capsuleCollider.enabled = false;
                animationBehaviour.PlayDeathAnimation();
                IsDead = true;
                actionScheduler.CancelCurrentAction();
            }
        }
    }
}