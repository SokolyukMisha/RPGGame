using UnityEngine;
using UnityEngine.Serialization;

namespace Main.CodeBase.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private AnimationBehaviour animationBehaviour;
        [SerializeField] private CapsuleCollider capsuleCollider;
        [SerializeField] private float health = 100f;

        public bool IsDead { get; private set; }

        public void TakeDamage(float damage)
        {
            if (IsDead) return;
            
            health -= damage;
            if (health <= 0)
            {
                capsuleCollider.enabled = false;
                animationBehaviour.PlayDeathAnimation();
                IsDead = true;
            }
        }
    }
}