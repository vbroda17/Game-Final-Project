using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistMovement : MonoBehaviour
{
    public BoxerMovement movement;
    public Transform leftArm;
    public Transform rightArm;

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
        MoveFist(leftArm, Direction.Left);
        MoveFist(rightArm, Direction.Right);
        CheckPunch();
    }

    void MoveFist(Transform arm, Direction direction)
    {
        Vector3 offset = Vector3.zero;
        if(direction == Direction.Left) offset.x = -.35f;
        if(direction == Direction.Right) offset.x = .35f;
        
        Vector3 move = Vector3.zero;
        if (Input.GetKey("up"))
            move.y = 1;
        if (Input.GetKey("down"))
            move.y = -1;
        if (Input.GetKey("left"))
            move.x = -.15f;
        if (Input.GetKey("right"))
            move.x = .15f;

        Vector3 newPosition = arm.localPosition + move;

        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        if(direction == Direction.Left) newPosition.x = Mathf.Clamp(newPosition.x, minX, originalPosition.x);
        if(direction == Direction.Right) newPosition.x = Mathf.Clamp(newPosition.x, originalPosition.x, maxX);
        arm.localPosition = newPosition;

        if (move == Vector3.zero)
        {
            arm.localPosition = originalPosition + offset;
        }
    }

    void CheckPunch()
    {
        if(Input.GetKey("q")) Punch(leftArm);
        if(Input.GetKey("e")) Punch(rightArm);
    }

void Punch(Transform arm)
{
    // Define punch duration and punch distance
    float punchDuration = 0.1f;
    float punchDistance = 0.5f;

    // Store original arm position
    Vector3 originalPosition = arm.localPosition;

    // Calculate target position for punch
    Vector3 targetPosition = originalPosition + Vector3.forward * punchDistance;

    // Perform punch animation or action
    StartCoroutine(PerformPunch(arm, originalPosition, targetPosition, punchDuration));
}

IEnumerator PerformPunch(Transform arm, Vector3 originalPosition, Vector3 targetPosition, float punchDuration)
{
    // Perform punch animation or action
    float elapsedTime = 0;
    while (elapsedTime < punchDuration)
    {
        arm.localPosition = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / punchDuration);
        elapsedTime += Time.deltaTime;
        yield return null; // Wait for the next frame
    }

    // Return arm to original position
    arm.localPosition = originalPosition;
}

}
