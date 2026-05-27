using UnityEngine;

public class JumpOrb : MonoBehaviour
{
    public float orbForce = 35f;

    private bool playerInside = false;
    private Jump playerJump;

    void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.Space))
        {
            if (playerJump != null)
            {
                playerJump.OrbJump(orbForce);
                playerInside = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Jump jumpScript = other.GetComponent<Jump>();

        if (jumpScript != null)
        {
            playerInside = true;
            playerJump = jumpScript;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Jump jumpScript = other.GetComponent<Jump>();

        if (jumpScript != null && jumpScript == playerJump)
        {
            playerInside = false;
            playerJump = null;
        }
    }
}