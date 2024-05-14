using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // public GameObject player;
    BoxerMovement boxerMovement;
    FistMovement boxerFists;
    BoxerKnockdown boxerKnockdown;
    Transform opponent;
    // Start is called before the first frame update
    void Start()
    {
        boxerMovement = GameObject.FindGameObjectWithTag("PlayerBoxer").GetComponent<BoxerMovement>();
        boxerFists = GameObject.FindGameObjectWithTag("PlayerBoxer").GetComponent<FistMovement>();
        boxerKnockdown = GameObject.FindGameObjectWithTag("PlayerBoxer").GetComponent<BoxerKnockdown>();

        opponent = GameObject.FindGameObjectWithTag("ComputerBoxer").transform;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PlayerJump();
        PlayerFistsMovement();
        PlayerPunch();
        PlayerRotationToggle();
        PlayerGetUp();
    }

    void PlayerMovement()
    {
        bool sprint = false;
        Vector3 move = new Vector3(0, 0, 0);
        if (Input.GetKey("w")) move.z = 1;
        if (Input.GetKey("s")) move.z = -1;
        if (Input.GetKey("a")) move.x = -1;
        if (Input.GetKey("d")) move.x = 1;
        if (Input.GetKey("left shift")) sprint = true;

        boxerMovement.HandleMovement(move, sprint);

        // For rotating the player in the direction of movement
        if(!boxerMovement.IsRotationTarget()) boxerMovement.RotatePlayerTowardsDirection(move);
        if(boxerMovement.IsRotationTarget()) boxerMovement.RotatePlayerTowardsTarget(opponent);
    }

    void PlayerJump()
    {
        if(Input.GetKey("space"))  boxerMovement.HandleJumping();
    }

    void PlayerFistsMovement()
    {
        Vector3 move = Vector3.zero;
        if (Input.GetKey("up"))
            move.y = 1;
        if (Input.GetKey("down"))
            move.y = -1;
        if (Input.GetKey("left"))
            move.x = -.15f;
        if (Input.GetKey("right"))
            move.x = .15f;

        boxerFists.MoveFists(move);
    }

    void PlayerPunch()
    {
        if(Input.GetKeyDown("q")) boxerFists.Punch(FistMovement.Direction.Left);
        if(Input.GetKeyDown("e")) boxerFists.Punch(FistMovement.Direction.Right);
    }

    void PlayerRotationToggle()
    {
        if(Input.GetKeyDown("r")) boxerMovement.SetIsRotatedTarget();   
    }

    void PlayerGetUp()
    {
        if (Input.GetKeyDown(KeyCode.Space) && boxerKnockdown.isKnockedDown)
        {
            boxerKnockdown.AttemptGetUp();
        }
    }
}
