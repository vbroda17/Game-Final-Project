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

    void Start(){}

    // Function for handling movement
    public void HandleMovement(Vector3 move, bool isSprinting)
    {
        float curSpeed = playerSpeed;
        if (isSprinting) curSpeed *= sprintMultiplier;

        controller.Move(move * Time.deltaTime * curSpeed);

        // For rotating the player in the direction of movement
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
    }

    // Function for handling jumping
    public void HandleJumping()
    {
        // Jumping
        if (groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        controller.Move(playerVelocity * Time.deltaTime);
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
