using System;
using Main.CodeBase.Enemy;
using UnityEngine;

namespace Main.CodeBase.Core
{
    public class CombatBehavior : MonoBehaviour
    {
        [SerializeField] private MovingBehaviour movingBehaviour;
        [SerializeField] private float attackDistance = 10f;
        
        private Transform _target;

        private void Update()
        {
            if (_target != null)
            {
                if (Vector3.Distance(transform.position, _target.position) <= attackDistance)
                {
                    movingBehaviour.Stop();
                }
                else
                {
                    movingBehaviour.MoveTo(_target.position);
                }
            }
                
        }

        public void Attack(EnemyController enemy)
        {
            _target = enemy.transform;
        }
        
        public void Stop()
        {
            _target = null;
        }
    }
}