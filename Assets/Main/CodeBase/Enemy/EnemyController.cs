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
    [RequireComponent(typeof(CombatBehavior))]
    [RequireComponent(typeof(Health))]
    public class EnemyController : MonoBehaviour
    {
        [Header("Require components")]
        [SerializeField] private MovingBehaviour movingBehaviour;
        [SerializeField] private CombatBehavior combatBehavior;
        [SerializeField] private Health health;
        [Header("AI settings")]
        [SerializeField] private PatrolWayPoint patrolWayPoint;
        [SerializeField] private float chasingSpeed = 5f;
        [SerializeField] private float patrolSpeed = 3f;
        [SerializeField] private float chaseDistance = 10f;
        [SerializeField] private float timeToWait = 5f;
        [SerializeField] private float waypointTolerance = 1f;
       
        private Vector3  _guardPosition;
        private float _lastPlayerSeenTime = Mathf.Infinity;
        private int _currentWayPointIndex = 0;
        private Transform _player;
        private void OnValidate()
        {
            movingBehaviour ??= GetComponent<MovingBehaviour>();
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
                ChasingBehaviour();
            }
            else if (_lastPlayerSeenTime < timeToWait)
                WaitingBehaviour();
            else
                PatrolBehaviour();

            _lastPlayerSeenTime += Time.deltaTime;
        }

        private void WaitingBehaviour()
        {
            combatBehavior.CancelAction();
        }

        private void ChasingBehaviour()
        {
            movingBehaviour.SetSpeed(chasingSpeed);
            _lastPlayerSeenTime = 0f;
            combatBehavior.Attack(_player.GetComponent<Health>());
        }

        private void PatrolBehaviour()
        {
            movingBehaviour.SetSpeed(patrolSpeed);
            Vector3 nextPosition = _guardPosition;
            if (patrolWayPoint != null)
            {
                if (AtWayPoint())
                {
                    _lastPlayerSeenTime = 0f;
                    CycleWayPoint();
                }

                nextPosition = GetCurrentWayPoint();
            }
            movingBehaviour.StartMoveAction(nextPosition);
        }
        
        private bool AtWayPoint()
        {
            float distanceToWayPoint = Vector3.Distance(transform.position, GetCurrentWayPoint());
            return distanceToWayPoint < waypointTolerance;
        }
        
        private void CycleWayPoint()
        {
            _currentWayPointIndex = patrolWayPoint.GetNextIndex(_currentWayPointIndex);
        }
        
        private Vector3 GetCurrentWayPoint()
        {
            return patrolWayPoint.GetWayPoint(_currentWayPointIndex);
        }

        private void OnDrawGizmos()
        {
            Handles.color = Color.red;
            Handles.DrawWireDisc(transform.position, Vector3.up, chaseDistance);
        }
    }
}