using System;
using Main.CodeBase.Core;
using Main.CodeBase.Core.Behaviour;
using Main.CodeBase.Enemy;
using UnityEngine;

namespace Main.CodeBase.Player
{
    [RequireComponent(typeof(CombatBehavior))]
    [RequireComponent(typeof(MovingBehaviour))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Require components")] 
        [SerializeField] private CombatBehavior combatBehavior;
        [SerializeField] private MovingBehaviour movingBehaviour;

        private Camera _camera;

        private void OnValidate()
        {
            combatBehavior??=GetComponent<CombatBehavior>();
            movingBehaviour??=GetComponent<MovingBehaviour>();
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if(CheckForInteraction()) return;
            if(MoveToCursor()) return;
            
        }

        private bool CheckForInteraction()
        {
            foreach (RaycastHit hit in Physics.RaycastAll(GetRay()))
            {
                if (hit.collider.TryGetComponent<EnemyController>(out var enemyController))
                {
                    if (Input.GetMouseButtonDown(0))
                        combatBehavior.Attack(enemyController.GetComponent<Health>());
                    return true;
                }
            }

            return false;
        }

        private bool MoveToCursor()
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = GetRay();
                bool hasHit = Physics.Raycast(ray, out RaycastHit hit);
                if (hasHit)
                    movingBehaviour.StartMoveAction(hit.point);
                return true;
            }

            return false;
        }

        private Ray GetRay() =>
            _camera.ScreenPointToRay(Input.mousePosition);
        
    }
}