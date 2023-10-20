using System;
using Main.CodeBase.Enemy;
using UnityEngine;
using UnityEngine.AI;

namespace Main.CodeBase.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Require components")] [SerializeField]
        private PlayerAnimator playerAnimator;

        [SerializeField] private PlayerAttack playerAttack;
        [SerializeField] private NavMeshAgent navMeshAgent;

        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                CheckForInteraction();
                MoveToCursor();
            }

            UpdateAnimation();
        }

        private void CheckForInteraction()
        {
            foreach (RaycastHit hit in Physics.RaycastAll(GetRay()))
            {
                if (hit.collider.TryGetComponent<EnemyController>(out var enemyController))
                {
                    if (Input.GetMouseButtonDown(0))
                        playerAttack.Attack();
                    break;
                }
            }
        }

        private void MoveToCursor()
        {
            Ray ray = GetRay();
            bool hasHit = Physics.Raycast(ray, out RaycastHit hit);
            if (hasHit)
                MoveTo(hit.point);
        }

        private Ray GetRay() =>
            _camera.ScreenPointToRay(Input.mousePosition);

        private void MoveTo(Vector3 destination) =>
            navMeshAgent.destination = destination;

        private void UpdateAnimation()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            playerAnimator.UpdateMovementAnimation(speed);
        }
    }
}