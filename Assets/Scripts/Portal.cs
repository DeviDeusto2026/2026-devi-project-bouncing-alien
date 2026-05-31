using UnityEngine;

public class Portal : MonoBehaviour
{
    [Header("Configuración del Destino")]
    [Tooltip("El objeto vacío (Transform) que marca dónde aparecerá el jugador")]
    public Transform destino;

    [Header("Gravedad del planeta destino")]
    public Gravity gravedadPlanetaDestino;

    private void OnTriggerEnter(Collider other)
    {
        Jump player = other.GetComponent<Jump>();
        Rigidbody rb = other.GetComponent<Rigidbody>();

        if (player != null && rb != null && destino != null)
        {
            player.currentPlatformZone = null;

            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            other.transform.position = destino.position;

            if (gravedadPlanetaDestino != null)
                gravedadPlanetaDestino.enabled = true;

            other.transform.rotation = Quaternion.LookRotation(Vector3.back, Vector3.up);
        }
    }
}