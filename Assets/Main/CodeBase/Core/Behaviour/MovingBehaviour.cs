using System;
using Main.CodeBase.Player;
using UnityEngine;
using UnityEngine.AI;


namespace Main.CodeBase.Core
{
    public class MovingBehaviour : MonoBehaviour, IAction
    {
        [SerializeField] private AnimationBehaviour animationBehaviour;
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private ActionScheduler actionScheduler;

        private void Update()
        {
            UpdateMovementAnimation();
        }
        
        public void StartMoveAction(Vector3 destination)
        {
            actionScheduler.StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector3 position)
        {
            agent.isStopped = false;
            agent.SetDestination(position);
        }
        
        public void CancelAction()
        {
            agent.isStopped = true;
        }

        private void UpdateMovementAnimation()
        {
            float speed = GetVelocity();
            animationBehaviour.UpdateMovementAnimation(speed);
        }

        private float GetVelocity()
        {
            Vector3 velocity = agent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            return localVelocity.z;
        }
    }
}