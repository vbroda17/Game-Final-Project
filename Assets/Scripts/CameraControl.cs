using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Transform player;
    Transform ring;
    bool isPlayerView;
    public float playerOffsetY = 2f;
    public float ringRotationSpeed = 100f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerBoxer").transform;
        ring = GameObject.FindGameObjectWithTag("Ring").transform;
        isPlayerView = true;
    }

    // Update is called once per frame
    void Update()
    {
        toggleCamera();
        moveCamera();
    }

    void toggleCamera()
    {
        if(Input.GetKeyDown("c")) 
        {
            isPlayerView =  !isPlayerView;
            if(!isPlayerView) RingCameraSetup();
        }
    }

    void moveCamera()
    {
        if(isPlayerView) PlayerCameraFirstPerson();
        if(!isPlayerView) RingCamera();
    }
    void PlayerCameraFirstPerson()
    {
        // Define the offset from the player
        Vector3 offset = new Vector3(0f, playerOffsetY, 0f); // Adjust as needed

        // Calculate the target position for the camera (same as player position + offset)
        Vector3 targetPosition = player.position + offset;

        // Set the camera's position to the calculated target position
        transform.position = targetPosition;

        // Set the camera's rotation to match the player's rotation
        transform.rotation = player.rotation;
    }

    void RingCamera()
    {
        // Define the distance from the center of the ring
        // Rotate around the ring's center over time
        transform.RotateAround(ring.position, Vector3.up, 20 * Time.deltaTime);
    }

    void RingCameraSetup()
    {
        float distanceFromRing = 25f;

        // Define the angle to look downwards (in degrees)
        float angle = 45f;

        // Calculate the camera position
        Vector3 cameraPosition = ring.position + new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad) * distanceFromRing, distanceFromRing, Mathf.Cos(angle * Mathf.Deg2Rad) * distanceFromRing);

        // Set the camera position
        transform.position = cameraPosition;

        // Make the camera look at the center of the ring
        transform.LookAt(ring.position);
    }

    // Additional Camera Anggles for possible later use
    void RingCameraTop()
    {
        // Define the distance from the center of the ring
        float distanceFromRing = 25f;

        // Calculate the camera position
        Vector3 cameraPosition = ring.position + new Vector3(0f, distanceFromRing, 0f);

        // Set the camera position
        transform.position = cameraPosition;

        // Make the camera look at the center of the ring
        transform.LookAt(ring.position);
    }

    void RingCameraFixedAngle()
    {
        // Define the distance from the center of the ring
        float distanceFromRing = 20f;

        // Define the angle to look downwards (in degrees)
        float angle = 30f;

        // Calculate the camera position
        Vector3 cameraPosition = ring.position + new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad) * distanceFromRing, distanceFromRing, Mathf.Cos(angle * Mathf.Deg2Rad) * distanceFromRing);

        // Set the camera position
        transform.position = cameraPosition;

        // Make the camera look at the center of the ring
        transform.LookAt(ring.position);
    }


}
