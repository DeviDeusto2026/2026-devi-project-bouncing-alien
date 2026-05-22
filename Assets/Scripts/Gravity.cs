using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float gravityForce = 25f;
    public float rotationSpeed = 4f;

    void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        Jump playerJump = other.GetComponent<Jump>(); // Buscamos el estado del jugador

        if (rb != null)
        {
            Vector3 gravityDirection;

            // ESTADO: ¿El jugador está en una plataforma?
            if (playerJump != null && playerJump.currentPlatformZone != null)
            {
                // Gravedad Plana: Tira hacia abajo relativo a la plataforma
                gravityDirection = -playerJump.currentPlatformZone.up;
            }
            else
            {
                // Gravedad Esférica: Tira hacia el centro del planeta
                gravityDirection = (transform.position - other.transform.position).normalized;
            }

            // Aplicamos la fuerza de gravedad correspondiente
            rb.AddForce(gravityDirection * gravityForce, ForceMode.Acceleration);

            // Rotamos al personaje para que sus pies apunten a la gravedad
            Quaternion targetRotation = Quaternion.FromToRotation(other.transform.up, -gravityDirection) * other.transform.rotation;
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }
    }
}