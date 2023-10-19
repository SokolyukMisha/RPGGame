using UnityEngine;
using UnityEngine.AI;

namespace Main.CodeBase.Player
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private Animator playerAnimator;
        private NavMeshAgent _navMeshAgent;
        
        private static readonly int Speed = Animator.StringToHash("Speed");

        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
            UpdateAnimation();
        }

        private void MoveToCursor()
        {
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit);
            if (hasHit) _navMeshAgent.destination = hit.point;
        }

        private void UpdateAnimation()
        {
            Vector3 velocity = _navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            playerAnimator.SetFloat(Speed, speed);
        }
    }
}