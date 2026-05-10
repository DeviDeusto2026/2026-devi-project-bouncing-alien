using UnityEngine;

public class PlanetCamera : MonoBehaviour
{
    public Transform jugador;
    public float distancia = 10f;
    public float altura = 5f;

    void FixedUpdate() // Cambia LateUpdate por FixedUpdate
    {
        if (jugador == null) return;

        Vector3 posicionDeseada = jugador.position + (jugador.up * altura) + (jugador.forward * distancia);

        // Usa un valor de suavizado (0.125f es un buen punto de partida)
        transform.position = Vector3.Lerp(transform.position, posicionDeseada, 0.125f);

        transform.LookAt(jugador.position, jugador.up);
    }
}