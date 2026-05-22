using UnityEngine;

public class PlanetCamera : MonoBehaviour
{
    public Transform jugador;

    public Vector3 offset = new Vector3(0f, 10f, -28f);
    public float suavizado = 5f;

    public float inclinacionAbajo = 10f;

    void LateUpdate()
    {
        if (jugador == null) return;

        // Sigue al alien, pero con offset fijo del mundo
        Vector3 posicionDeseada = jugador.position + offset;

        transform.position = Vector3.Lerp(
            transform.position,
            posicionDeseada,
            suavizado * Time.deltaTime
        );

        // Cámara fija, solo un poquito inclinada hacia abajo
        transform.rotation = Quaternion.Euler(inclinacionAbajo, 0f, 0f);
    }
}