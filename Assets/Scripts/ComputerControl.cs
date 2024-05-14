using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerControl : MonoBehaviour
{
    BoxerMovement boxerMovement;
    FistMovement boxerFists;
    BoxerKnockdown boxerKnockdown;
    Transform opponent;

    public KnockdownCount knockdownCount;

    void Start()
    {
        boxerMovement = GameObject.FindGameObjectWithTag("ComputerBoxer").GetComponent<BoxerMovement>();
        boxerFists = GameObject.FindGameObjectWithTag("ComputerBoxer").GetComponent<FistMovement>();
        boxerKnockdown = GameObject.FindGameObjectWithTag("ComputerBoxer").GetComponent<BoxerKnockdown>();
        opponent = GameObject.FindGameObjectWithTag("PlayerBoxer").transform;

        StartCoroutine(CheckGetUp());
    }

    void Update()
    {
        ComputerMovement();
        ComputerPunch();
    }

void ComputerMovement()
{
    // // Move towards the player boxer
    // boxerMovement.HandleMovement(directionToPlayer, false);
    if(!knockdownCount.countStarted) boxerMovement.MoveTowardsTarget(opponent, boxerMovement.playerSpeed);
    if(knockdownCount.countStarted) boxerMovement.MoveAwayFromTarget(opponent, boxerMovement.playerSpeed); // I want this to move to the oposite
    boxerMovement.RotatePlayerTowardsTarget(opponent);
}

    void ComputerPunch()
    {
        if(boxerFists.isPunching || boxerKnockdown.isKnockedDown) return;
        FistMovement.Direction punchDirection = (Random.value < 0.5f) ? FistMovement.Direction.Left : FistMovement.Direction.Right;

        // Perform the punch
        boxerFists.Punch(punchDirection);
        
        StartCoroutine(DelayNextPunch());
    }

    IEnumerator DelayNextPunch()
    {
        // Wait for one second before the next punch
        yield return new WaitForSeconds(1f);

        // Call ComputerPunch again to perform the next punch after the delay
        ComputerPunch();
    }

    IEnumerator CheckGetUp()
    {
        while (!knockdownCount.gameOver)
        {
            // Check if the computer is knocked down and not already getting up
            if (boxerKnockdown.isKnockedDown && !knockdownCount.gameOver)
            {
                // Randomly wait between 0.5 and 2 seconds before attempting to get up
                float randomWaitTime = Random.Range(0.2f, 2.1f);
                print(randomWaitTime);
                yield return new WaitForSeconds(randomWaitTime);
                if(knockdownCount.gameOver) yield break;
                // Attempt to get up
                boxerKnockdown.AttemptGetUp();
            }

            yield return null;
        }
    }
}