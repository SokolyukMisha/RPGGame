using System;
using Main.CodeBase.Core;
using Main.CodeBase.Core.Behaviour;
using Main.CodeBase.Player;
using UnityEngine;

namespace Main.CodeBase.Enemy
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(MovingBehaviour))]
    [RequireComponent(typeof(AnimationBehaviour))]
    [RequireComponent(typeof(CombatBehavior))]
    public class EnemyController : MonoBehaviour
    {
        [Header("Require components")]
        [SerializeField] private MovingBehaviour movingBehaviour;
        [SerializeField] private AnimationBehaviour animationBehaviour;
        [SerializeField] private CombatBehavior combatBehavior;
        
        [Header("AI settings")]
        [SerializeField] private float chaseDistance = 10f;
        
        private Transform _player;

        private void OnValidate()
        {
            movingBehaviour ??= GetComponent<MovingBehaviour>();
            animationBehaviour ??= GetComponent<AnimationBehaviour>();
            combatBehavior ??= GetComponent<CombatBehavior>();
        }

        private void Awake()
        {
            _player = FindObjectOfType<PlayerController>().transform;
        }
        
        private void Update()
        {
            float distanceToPlayer = Vector3.Distance(_player.position, transform.position);
            if (distanceToPlayer < chaseDistance)
            {
                combatBehavior.Attack(_player.GetComponent<Health>());
            }
            else
            {
                combatBehavior.CancelAction();
            }
        }
    }
}