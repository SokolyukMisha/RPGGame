using System;
using Main.CodeBase.Enemy;
using UnityEngine;

namespace Main.CodeBase.Core
{
    public class CombatBehavior : MonoBehaviour, IAction
    {
        [SerializeField] private MovingBehaviour movingBehaviour;
        [SerializeField] private ActionScheduler actionScheduler;
        [SerializeField] private float attackDistance = 10f;


        private Transform _target;

        private void Update()
        {
            if (_target != null)
            {
                if (Vector3.Distance(transform.position, _target.position) <= attackDistance)
                {
                    movingBehaviour.CancelAction();
                }
                else
                {
                    movingBehaviour.MoveTo(_target.position);
                }
            }
                
        }

        public void Attack(EnemyController enemy)
        {
            actionScheduler.StartAction(this);
            _target = enemy.transform;
        }
        
        public void CancelAction()
        {
            _target = null;
        }
    }
}