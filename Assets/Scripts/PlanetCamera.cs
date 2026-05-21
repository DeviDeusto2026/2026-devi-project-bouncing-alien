using UnityEngine;

public class PlanetCamera : MonoBehaviour
{
    public Transform jugador;

    public float distancia = 18f;
    public float altura = 8f;
    public float suavizado = 5f;

    void LateUpdate()
    {
        if (jugador == null) return;

        // Cámara más alejada y más alta
        Vector3 posicionDeseada =
            jugador.position
            + jugador.up * altura
            - jugador.forward * distancia;

        transform.position = Vector3.Lerp(
            transform.position,
            posicionDeseada,
            suavizado * Time.deltaTime
        );

        // Mira al alien, pero un poco por encima para ver más escenario
        Vector3 puntoMirada = jugador.position + jugador.up * 2f;

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(puntoMirada - transform.position, jugador.up),
            suavizado * Time.deltaTime
        );
    }
}