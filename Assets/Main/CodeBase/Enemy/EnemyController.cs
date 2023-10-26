using System;
using Main.CodeBase.Core;
using Main.CodeBase.Core.Behaviour;
using Main.CodeBase.Player;
using UnityEditor;
using UnityEngine;

namespace Main.CodeBase.Enemy
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(MovingBehaviour))]
    [RequireComponent(typeof(AnimationBehaviour))]
    [RequireComponent(typeof(CombatBehavior))]
    [RequireComponent(typeof(Health))]
    public class EnemyController : MonoBehaviour
    {
        [Header("Require components")]
        [SerializeField] private MovingBehaviour movingBehaviour;
        [SerializeField] private AnimationBehaviour animationBehaviour;
        [SerializeField] private CombatBehavior combatBehavior;
        [SerializeField] private Health health;
        [Header("AI settings")]
        [SerializeField] private float chaseDistance = 10f;
        [SerializeField] private float timeToWait = 5f;
       
        private Vector3  _guardPosition;
        private float _lastPlayerSeenTime = Mathf.Infinity;
        private Transform _player;

        private void OnValidate()
        {
            movingBehaviour ??= GetComponent<MovingBehaviour>();
            animationBehaviour ??= GetComponent<AnimationBehaviour>();
            combatBehavior ??= GetComponent<CombatBehavior>();
            health ??= GetComponent<Health>();
        }

        private void Awake()
        {
            _player = FindObjectOfType<PlayerController>().transform;
            _guardPosition = transform.position;
        }
        
        private void Update()
        {
            if(health.IsDead) return;
            float distanceToPlayer = Vector3.Distance(_player.position, transform.position);
            if (distanceToPlayer < chaseDistance)
            {
                _lastPlayerSeenTime = 0f;
                combatBehavior.Attack(_player.GetComponent<Health>());
            }
            else if (_lastPlayerSeenTime < timeToWait)
                combatBehavior.CancelAction();
            else
                movingBehaviour.MoveTo(_guardPosition);

            _lastPlayerSeenTime += Time.deltaTime;
        }

        private void OnDrawGizmos()
        {
            Handles.color = Color.red;
            Handles.DrawWireDisc(transform.position, Vector3.up, chaseDistance);
        }
    }
}