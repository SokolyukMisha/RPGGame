using System;
using UnityEngine;

namespace Main.CodeBase.Core.Behaviour
{
    [RequireComponent(typeof(MovingBehaviour))]
    [RequireComponent(typeof(ActionScheduler))]
    [RequireComponent(typeof(AnimationBehaviour))]
    public class CombatBehavior : MonoBehaviour, IAction
    {
        [Header("Required Components")] 
        [SerializeField] private MovingBehaviour movingBehaviour;
        [SerializeField] private ActionScheduler actionScheduler;
        [SerializeField] private AnimationBehaviour animationBehaviour;

        [Header("Attack parameters")] 
        [SerializeField] private float attackDistance = 10f;
        [SerializeField] private float attackSpeed = 1f;
        [SerializeField] private float damage = 1f;
        
        private Health _target;
        private float _attackTimer;

        private void OnValidate()
        {
            movingBehaviour??=GetComponent<MovingBehaviour>();
            actionScheduler??=GetComponent<ActionScheduler>();
            animationBehaviour??=GetComponent<AnimationBehaviour>();
        }

        private void Update()
        {
            _attackTimer += Time.deltaTime;
            if (_target != null)
            {
                if (Vector3.Distance(transform.position, _target.transform.position) <= attackDistance)
                {
                    movingBehaviour.CancelAction();
                    transform.LookAt(_target.transform);
                    DoAttack();
                }
                else
                {
                    movingBehaviour.MoveTo(_target.transform.position);
                }
            }
        }

        private void DoAttack()
        {
            if (_attackTimer < attackSpeed) return;
            animationBehaviour.PlayAttackAnimation();
            _attackTimer = 0f;
        }

        public void Attack(Health enemy)
        {
            actionScheduler.StartAction(this);
            _target = enemy;
        }

        public void CancelAction()
        {
            _target = null;
            animationBehaviour.StopAttackAnimation();
        }

        public void Hit()
        {
            if(_target == null) return;
            _target.TakeDamage(damage);
            if (_target.IsDead)
                CancelAction();
        }
    }
}