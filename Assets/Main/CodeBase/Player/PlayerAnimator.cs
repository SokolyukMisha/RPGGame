using UnityEngine;

namespace Main.CodeBase.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator playerAnimator;
        
        private static readonly int Speed = Animator.StringToHash("Speed");

        public void UpdateMovementAnimation(float speed)
        {
            playerAnimator.SetFloat(Speed, speed);
        }
    }
}