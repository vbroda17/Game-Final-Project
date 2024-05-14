using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxerKnockdown : MonoBehaviour
{
    BoxerHealth boxerHealth;
    public bool isKnockedDown = false;
    public int knockDownHealthMax = 10;
    private int knockDownHealth;


    // Start is called before the first frame update
    void Start()
    {
        boxerHealth = GetComponent<BoxerHealth>();
        boxerHealth.OnHealthZero += Knockdown; // Subscribe to the OnHealthZero event
    }

    void Knockdown()
    {
        if (!isKnockedDown)
        {
            isKnockedDown = true;
            // Disable boxer movement
            // Assuming BoxerMovement is a script that controls movement, you may need to adjust this according to your setup
            GetComponent<BoxerMovement>().enabled = false;

            knockDownHealth = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isKnockedDown)
        {
            // Rotate the boxer to be parallel with the ground
            transform.rotation = Quaternion.Euler(90f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            // transform.Translate(Vector3.down * knockdownYOffset, Space.Self);

        }
    }

    public void AttemptGetUp()
    {
        knockDownHealth++;
        if (knockDownHealth >= knockDownHealthMax) PerformGetUp();
    }

    void PerformGetUp()
    {
        // Perform actions to get up
        // For example, enable boxer movement again
        GetComponent<BoxerMovement>().enabled = true;
        // Reset knocked down flag
        isKnockedDown = false;
        boxerHealth.RestoreAllHealth();
    }
}