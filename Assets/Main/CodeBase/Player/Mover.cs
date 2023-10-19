using System;
using UnityEngine;
using UnityEngine.AI;

namespace Main.CodeBase.Player
{
    public class Mover : MonoBehaviour
    {
        private Transform _target;
        private NavMeshAgent _navMeshAgent;

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
        }

        private void MoveToCursor()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit);
            if (hasHit)
            {
                _target = hit.transform;
                _navMeshAgent.destination = hit.point;
            }
        }
    }
}