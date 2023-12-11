using UnityEditor;
using UnityEngine;

namespace Main.CodeBase.Enemy
{
    public class PatrolWayPoint : MonoBehaviour
    {
        [SerializeField] private float gizmoRadius = 0.3f;

        private void OnDrawGizmos()
        {
            GUIStyle labelStyle = new GUIStyle
            {
                fontSize = 20,
                normal =
                {
                    textColor = Color.red
                }
            };
            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.DrawSphere(transform.GetChild(i).position, gizmoRadius);
                int index = i + 1;
                Handles.Label(transform.GetChild(i).position, index.ToString(), labelStyle);

                DrawLine(i);
            }
        }

        private void DrawLine(int i)
        {
            Vector3 position = transform.GetChild(i).position;
            Vector3 nextPosition = transform.GetChild(GetNextIndex(i)).position;
            Gizmos.DrawLine(position, nextPosition);
        }

        public int GetNextIndex(int i)
        {
            if (i + 1 >= transform.childCount)
                return 0;
            return i + 1;
        }

        public Vector3 GetWayPoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}