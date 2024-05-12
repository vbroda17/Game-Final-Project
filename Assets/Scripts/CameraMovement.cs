using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 2, -20);


    // Update is called once per frame
    void Update()
    {
        // Set our position to the players position and offset it
        transform.position = player.transform.position + offset;
    }
}
