using UnityEngine;

public class Gravity : MonoBehaviour
{
    [Header("Configuración de Gravedad")]
    public float gravityForce = 35f;
    public float rotationSpeed = 8f;

    void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        Jump playerJump = other.GetComponent<Jump>();

        if (rb != null)
        {
            Vector3 gravityDirection;

            // PRIORIDAD 1: El jugador entra en el Túnel de Inversión
            if (playerJump != null && playerJump.currentPlatformZone != null)
            {
                if (GetComponent<Platform>() == null) return;

                // 1. La fuerza lo "chupa" hacia el Planeta 2 (hacia arriba en el mapa global)
                gravityDirection = Vector3.up;

                // 2. Aplicamos la fuerza del planeta de destino para que lo atraiga
                Platform platformScript = playerJump.currentPlatformZone.GetComponent<Platform>();
                float finalForce = gravityForce;
                if (platformScript != null)
                {
                    finalForce *= platformScript.forceMultiplier;
                }
                rb.AddForce(gravityDirection * finalForce, ForceMode.Acceleration);

                // 3. ¡LA MAGIA DEL GIRO INVERTIDO!
                // Hacemos que el personaje se oriente alineando su cabeza hacia el Planeta 1 
                // y sus pies mirando hacia el Planeta 2 (boca abajo respecto al mundo entero)
                Quaternion invertRotation = Quaternion.FromToRotation(other.transform.up, gravityDirection) * other.transform.rotation;

                // Forzamos el giro rápido para que se dé la vuelta en el aire inmediatamente al entrar
                rb.MoveRotation(Quaternion.Slerp(rb.rotation, invertRotation, rotationSpeed * 2f * Time.fixedDeltaTime));

                return;
            }
            // PRIORIDAD 2: Gravedad esférica normal de los planetas
            else
            {
                if (GetComponent<Platform>() != null) return;

                Gravity[] allPlanets = Object.FindObjectsByType<Gravity>(FindObjectsSortMode.None);
                Gravity closestPlanet = this;
                float closestDistance = Vector3.Distance(transform.position, other.transform.position);

                foreach (Gravity planet in allPlanets)
                {
                    if (planet.GetComponent<Platform>() != null) continue;

                    float distance = Vector3.Distance(planet.transform.position, other.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestPlanet = planet;
                    }
                }

                if (closestPlanet != this) return;

                gravityDirection = (transform.position - other.transform.position).normalized;
                rb.AddForce(gravityDirection * gravityForce, ForceMode.Acceleration);
            }

            // Aplica la rotación normal de los planetas
            Quaternion targetRotation = Quaternion.FromToRotation(other.transform.up, -gravityDirection) * other.transform.rotation;
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }
    }
}