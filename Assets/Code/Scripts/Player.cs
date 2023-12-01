using UnityEngine;

namespace Shooter
{
    public struct Player
    {
        public Transform playerTransform;
        public Animator playerAnimator;
        public Rigidbody playerRigidbody;
        public float playerSpeed;
    }
}