using UnityEngine;

namespace Main.CodeBase.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float health = 100f;
        
        public void TakeDamage(float damage)
        {
            health -= damage;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}