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
    public float jumpHeight = 1.0f;

    void Start(){}

    // Update is called once per frame
    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Movement v
        Vector3 move = new Vector3(0, 0, 0);
        if(Input.GetKey("w")) move.z = 1;
        if(Input.GetKey("s")) move.z = -1;
        if(Input.GetKey("a")) move.x = -1;
        if(Input.GetKey("d")) move.x = 1;
        float curSpeed = playerSpeed;
        if(Input.GetKey("left shift")) curSpeed *= sprintMultiplier;
        
        controller.Move(move * Time.deltaTime * curSpeed);

        // For Jumping
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (Input.GetKey("space") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

}
