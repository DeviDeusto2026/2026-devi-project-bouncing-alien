using UnityEngine;

public class Portal : MonoBehaviour
{
    [Header("Configuración del Destino")]
    [Tooltip("El objeto vacío (Transform) que marca dónde aparecerá el jugador")]
    public Transform destino;

    private void OnTriggerEnter(Collider other)
    {
        // Comprobamos si lo que ha entrado es el jugador (comprobando el script Jump)
        Jump player = other.GetComponent<Jump>();
        Rigidbody rb = other.GetComponent<Rigidbody>();

        if (player != null && rb != null && destino != null)
        {
            // 1. Teletransportamos al jugador al destino
            other.transform.position = destino.position;

            // 2. IMPORTANTE: Reseteamos su velocidad para que aparezca estable
            rb.linearVelocity = Vector3.zero; // Usa rb.velocity si estás en Unity antiguo
            rb.angularVelocity = Vector3.zero;

            // 3. Lo orientamos mirando a la cámara por defecto al aparecer
            other.transform.rotation = Quaternion.LookRotation(Vector3.back, Vector3.up);
        }
    }
}