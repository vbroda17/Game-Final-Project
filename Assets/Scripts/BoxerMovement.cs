using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxerMovement : MonoBehaviour
{
    public CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed = 2.0f;
    public float sprintMultiplier = 1.5f;
    public float gravityValue = -9.81f;
    public float jumpHeight = .5f;
    private bool isRotationTarget;
    private BoxerKnockdown boxerKnockdown;
    void Start()
    {
        isRotationTarget = false;
        boxerKnockdown = GetComponentInParent<BoxerKnockdown>();
    }

    // Function for handling movement
    public void HandleMovement(Vector3 move, bool isSprinting)
    {
        if (!boxerKnockdown.isKnockedDown) // Check if the boxer is not knocked down
        {
            float curSpeed = playerSpeed;
            if (isSprinting) curSpeed *= sprintMultiplier;

            controller.Move(move * Time.deltaTime * curSpeed);
        }
    }

    public void MoveTowardsTarget(Transform target, float speed)
    {
        if (!boxerKnockdown.isKnockedDown) // Check if the boxer is not knocked down
        {
            if (target != null)
            {
                Vector3 direction = (target.position - transform.position).normalized;
                controller.Move(direction * Time.deltaTime * speed);
            }
        }
    }

    // Function to rotate the player towards a direction vector
    public void RotatePlayerTowardsDirection(Vector3 direction)
    {
        if (!boxerKnockdown.isKnockedDown) // Check if the boxer is not knocked down
        {
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5f); // Adjust rotation speed as needed
            }
        }
    }


    // Function to rotate the player towards a target transform
    public void RotatePlayerTowardsTarget(Transform target)
    {
        if (!boxerKnockdown.isKnockedDown) // Check if the boxer is not knocked down
        {
            if (target != null)
            {
                Vector3 direction = target.position - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5f); // Adjust rotation speed as needed
            }
        }
    }

    public bool SetIsRotatedTarget()
    {
        isRotationTarget = !isRotationTarget;
        return isRotationTarget;
    }

    public bool IsRotationTarget()
    {
        return isRotationTarget;
    }


    // Function for handling jumping
    public void HandleJumping()
    {
        if (!boxerKnockdown.isKnockedDown) // Check if the boxer is not knocked down
        {
            // Jumping
            if (groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }
            controller.Move(playerVelocity * Time.deltaTime);
        }
    }

// Function for handling gravity
    public void ApplyGravity()
    {
        groundedPlayer = controller.isGrounded;
        
        // Reset vertical velocity to zero if grounded and moving downwards
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Apply gravity
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }


    // Update is called once per frame
    void Update()
    {
        ApplyGravity();
    }
}
