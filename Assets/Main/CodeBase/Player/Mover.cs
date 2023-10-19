using UnityEngine;
using UnityEngine.AI;

namespace Main.CodeBase.Player
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private void Update()
        {
            GetComponent<NavMeshAgent>().destination = target.position;
        }
    }
}
