using System;
using Main.CodeBase.Enemy;
using UnityEngine;

namespace Main.CodeBase.Core
{
    public class CombatBehavior : MonoBehaviour, IAction
    {
        [Header("Required Components")] [SerializeField]
        private MovingBehaviour movingBehaviour;

        [SerializeField] private ActionScheduler actionScheduler;
        [SerializeField] private AnimationBehaviour animationBehaviour;

        [Header("Attack parameters")] [SerializeField]
        private float attackDistance = 10f;

        [SerializeField] private float attackSpeed = 1f;
        [SerializeField] private float damage = 1f;


        private Transform _target;
        private float _attackTimer;

        private void Update()
        {
            _attackTimer += Time.deltaTime;
            if (_target != null)
            {
                if (Vector3.Distance(transform.position, _target.position) <= attackDistance)
                {
                    movingBehaviour.CancelAction();
                    transform.LookAt(_target);
                    DoAttack();
                }
                else
                {
                    movingBehaviour.MoveTo(_target.position);
                }
            }
        }

        private void DoAttack()
        {
            if (_attackTimer < attackSpeed) return;
            animationBehaviour.PlayAttackAnimation();
            _attackTimer = 0f;
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

        public void Hit()
        {
            _target.GetComponent<Health>().TakeDamage(damage);
        }
    }
}