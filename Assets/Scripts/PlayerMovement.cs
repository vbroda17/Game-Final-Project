using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float forwardForce = 1000f;
    public float sidewaysForce = 100f;
    public float maxForce = 20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // Limiting speed
        var curVelocity = rb.velocity;

        if (Input.GetKey("up")) // If the player is pressing the "right" key
        {
            if(curVelocity.z < maxForce)
                rb.AddForce(0f, 0f, sidewaysForce * Time.deltaTime, ForceMode.VelocityChange);
        }
        if (Input.GetKey("down")) // If the player is pressing the "left" key
        {
            // Add a force to the left
            if (curVelocity.z > -maxForce)
                rb.AddForce(0f, 0f, -sidewaysForce * Time.deltaTime, ForceMode.VelocityChange);
        }

        if (Input.GetKey("right")) // If the player is pressing the "right" key
        {
            if (curVelocity.z < maxForce)
                rb.AddForce(sidewaysForce * Time.deltaTime, 0f, 0f, ForceMode.VelocityChange);
        }
        if (Input.GetKey("left")) // If the player is pressing the "left" key
        {
            if (curVelocity.x > -maxForce)
                rb.AddForce(-sidewaysForce * Time.deltaTime, 0f, 0f, ForceMode.VelocityChange);
        }

        // bound check
        if(rb.position.y < 0f) 
        {
            Vector3 respawn = new Vector3(0, 10, 0);
            rb.position = respawn;
        }
    }
}
