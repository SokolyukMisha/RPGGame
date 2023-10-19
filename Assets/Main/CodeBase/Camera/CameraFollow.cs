using System;
using UnityEngine;

namespace Main.CodeBase.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [Header("FollowSettings")]
        [SerializeField] private Transform followTarget;
        [SerializeField] private float followSpeed = 10f;
        [SerializeField] private float followDistance = 10f;
        [SerializeField] private float followHeight = 10f;
        [SerializeField] private float followAngle = 10f;
        

        private void LateUpdate()
        {
            if (followTarget == null)
                return;
            
            Vector3 targetPosition = followTarget.position;
            Vector3 direction = Quaternion.Euler(followAngle, 0, 0) * Vector3.back;
            Vector3 position = targetPosition + direction * followDistance + Vector3.up * followHeight;
            transform.position = Vector3.Lerp(transform.position, position, followSpeed * Time.deltaTime);
            transform.LookAt(targetPosition);
        }
    }
}