using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistMovement : MonoBehaviour
{
    public BoxerMovement movement;
    public Transform leftArm;
    public Transform rightArm;

    public bool isPunching = false; // Flag to track if the punching animation is playing

    // Define boundaries for arm movement
    private float minX = -0.5f;
    private float maxX = 0.5f;
    private float minY = 1.5f;
    private float maxY = 2.5f;
    private Vector3 originalPosition;
    public enum Direction
    {
        Left,
        Right
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 originalPositionLeft= leftArm.localPosition;
        Vector3 originalPositionRight = rightArm.localPosition;
        originalPosition = (originalPositionLeft + originalPositionRight) / 2;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void MoveFists(Vector3 movement)
    {
        if (!isPunching) // Only allow movement if not currently punching
        {
            MoveFist(leftArm, Direction.Left, movement);
            MoveFist(rightArm, Direction.Right, movement);
        }
    }

    void MoveFist(Transform arm, Direction direction, Vector3 move)
    {
        Vector3 offset = Vector3.zero;
        if(direction == Direction.Left) offset.x = -.35f;
        if(direction == Direction.Right) offset.x = .35f;
        arm.localPosition = originalPosition + offset;
        
        Vector3 newPosition = arm.localPosition + move;

        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        if(direction == Direction.Left) newPosition.x = Mathf.Clamp(newPosition.x, minX, originalPosition.x);
        if(direction == Direction.Right) newPosition.x = Mathf.Clamp(newPosition.x, originalPosition.x, maxX);
        arm.localPosition = newPosition;

        if (move == Vector3.zero)
        {
            arm.localPosition = originalPosition + offset;
        }

        Rigidbody armRB = arm.GetComponent<Rigidbody>();
        armRB.MovePosition(arm.position);
        armRB.MoveRotation(arm.rotation);
    }

    public void Punch(Direction direction)
    {
        if(isPunching) return;
        if(direction == Direction.Left) StartCoroutine(PerformPunch(leftArm));
        if(direction == Direction.Right) StartCoroutine(PerformPunch(rightArm));
    }

IEnumerator PerformPunch(Transform arm)
{
    isPunching = true; // Set punching flag to true

    // Store original arm position and rotation
    Vector3 originalPosition = arm.localPosition;
    Quaternion originalRotation = arm.localRotation;

    // Define punch duration and punch distance
    float punchDuration = 0.5f;
    float punchDistance = 1f;
    float punchRotation = 90f; // Rotation angle for the punch

    // Calculate target position for punch
    Vector3 targetPosition = originalPosition + Vector3.forward * punchDistance;

    // Calculate target rotation for punch
    Quaternion targetRotation = Quaternion.Euler(punchRotation, 0, 0);

    // Perform punch animation or action
    float elapsedTime = 0;
    while (elapsedTime < punchDuration)
    {
        // Move arm
        arm.localPosition = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / punchDuration);
        // Rotate arm
        arm.localRotation = Quaternion.Slerp(originalRotation, targetRotation, elapsedTime / punchDuration);

        elapsedTime += Time.deltaTime;
        yield return null; // Wait for the next frame
    }

    // Smoothly return arm to original position and rotation
    float returnDuration = 0.5f;
    float returnElapsedTime = 0;
    while (returnElapsedTime < returnDuration)
    {
        // Move arm back to original position
        arm.localPosition = Vector3.Lerp(targetPosition, originalPosition, returnElapsedTime / returnDuration);
        // Rotate arm back to original rotation
        arm.localRotation = Quaternion.Slerp(targetRotation, originalRotation, returnElapsedTime / returnDuration);

        returnElapsedTime += Time.deltaTime;
        yield return null; // Wait for the next frame
    }

    isPunching = false; // Reset punching flag
}

}
