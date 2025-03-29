using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{ public class MovementManager : MonoBehaviour
    {
        [SerializeField] private Joystick joystick;
        [SerializeField] private Rigidbody playerRigidbody;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private Animator playerAnimator;


        private void FixedUpdate()
        {
            float moveX = joystick.Horizontal;
            float moveZ = joystick.Vertical;

            Vector3 moveDirection = new Vector3(moveX, 0, moveZ).normalized;

            playerRigidbody.velocity = moveDirection * moveSpeed + new Vector3(0, playerRigidbody.velocity.y, 0);
            playerAnimator.SetFloat("Velocity", playerRigidbody.velocity.magnitude);
            if (moveDirection != Vector3.zero)
            {
                playerTransform.forward = moveDirection;
            }
        }
    }
}