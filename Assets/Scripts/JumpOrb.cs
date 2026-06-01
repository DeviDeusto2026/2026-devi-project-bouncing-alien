using UnityEngine;

public class JumpOrb : MonoBehaviour
{
    public float orbForce = 35f;

    [Header("Audio Settings")]
    public AudioClip bounceSound;

    private bool isPlayerInside = false;
    private Jump playerJump;

    void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.Space))
        {
            if (playerJump != null)
            {
                playerJump.OrbJump(orbForce);

                if (bounceSound != null)
                {
                    float sliderVolume = PlayerPrefs.GetFloat("Volume", 0.75f);
                    float muteMultiplier = PlayerPrefs.GetFloat("MuteMultiplier", 1f);
                    float finalVolume = sliderVolume * muteMultiplier;

                    AudioSource.PlayClipAtPoint(bounceSound, transform.position, finalVolume);
                }

                isPlayerInside = false;
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        Jump jumpScript = collider.GetComponent<Jump>();

        if (jumpScript != null)
        {
            isPlayerInside = true;
            playerJump = jumpScript;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        Jump jumpScript = collider.GetComponent<Jump>();

        if (jumpScript != null && jumpScript == playerJump)
        {
            isPlayerInside = false;
            playerJump = null;
        }
    }
}