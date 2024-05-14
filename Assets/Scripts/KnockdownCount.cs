using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnockdownCount : MonoBehaviour
{
    private BoxerKnockdown player;
    private BoxerKnockdown computer;
    public Text knockdownCount;
    private bool countStarted = false; // Variable to track if counting has started
    public bool gameOver = false; // Variable to track if counting has started


    void Start()
    {
        // Get references to player and computer BoxerKnockdown scripts
        player = GameObject.FindGameObjectWithTag("PlayerBoxer").GetComponent<BoxerKnockdown>();
        computer = GameObject.FindGameObjectWithTag("ComputerBoxer").GetComponent<BoxerKnockdown>();

        // Ensure the knockdown count is initialized properly
        UpdateKnockdownText();
    }

    void Update()
    {
        // Check for knockdowns every frame
        CheckDown();
    }

    void CheckDown()
    {
        // Check if either player or computer is knocked down and counting has not started
        if ((player.isKnockedDown || computer.isKnockedDown) && !countStarted && !gameOver)
        {
            // Set countStarted to true to prevent re-triggering the counting process
            countStarted = true;

            // Display "DOWN" when either boxer is knocked down
            knockdownCount.text = "DOWN";

            // Start the coroutine to count up from "ONE" to "TEN"
            StartCoroutine(CountUp());
        }
    }

    IEnumerator CountUp()
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second after "DOWN" is displayed

        // Count up from "ONE" to "TEN"
        for (int i = 1; i <= 10; i++)
        {
            if (!player.isKnockedDown && !computer.isKnockedDown)
            {
                countStarted = false;
                knockdownCount.text = "HE'S UP!!";
                yield break;
            }
            knockdownCount.text = i.ToString();
            yield return new WaitForSeconds(1f); // Wait for 1 second before displaying the next count
        }

        gameOver = true;

        // Check the knockdown status of players after the count
        if (player.isKnockedDown)
        {
            // Player was knocked down, so display "Lose"
            knockdownCount.text = "You lost, boxing is an unpredictable sport...";
        }
        else if (computer.isKnockedDown)
        {
            // Computer was knocked down, so display "Win"
            knockdownCount.text = "You are now THE CHAMP!";
        }

        // Reset countStarted after counting is complete
        countStarted = false;
    }

    void UpdateKnockdownText()
    {
        // Update the knockdown count text based on the current state of the player and computer knockdown flags
        if (player.isKnockedDown || computer.isKnockedDown)
        {
            knockdownCount.text = "DOWN";
        }
        else
        {
            knockdownCount.text = "";
        }
    }
}