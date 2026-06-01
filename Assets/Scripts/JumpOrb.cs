using UnityEngine;

public class JumpOrb : MonoBehaviour
{
    public float orbForce = 35f;
    [Header("Configuración de Sonido")]
    public AudioClip bounceSound;

    private bool playerInside = false;
    private Jump playerJump;

    void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.Space))
        {
            if (playerJump != null)
            {
                playerJump.OrbJump(orbForce);

                // Reproduce el sonido respetando el control de volumen y el botón de Mute
                if (bounceSound != null)
                {
                    // 1. Leemos el volumen del Slider (por defecto 0.75f si no existe)
                    float volumenSlider = PlayerPrefs.GetFloat("Volume", 0.75f);

                    // 2. Leemos si el botón de mute está activado (por defecto 1f si no existe)
                    float multiplicadorMute = PlayerPrefs.GetFloat("MuteMultiplier", 1f);

                    // 3. Calculamos el volumen final real (Volumen x Mute)
                    float volumenFinal = volumenSlider * multiplicadorMute;

                    AudioSource.PlayClipAtPoint(bounceSound, transform.position, volumenFinal);
                }

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