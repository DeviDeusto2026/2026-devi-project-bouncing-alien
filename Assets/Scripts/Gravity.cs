using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float gravedad = -800f; // Fuerza potente para que no flote

    void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 direccionHaciaCentro = (transform.position - other.transform.position).normalized;

            // Si el alien se está alejando del planeta (saltando), 
            // aplicamos un poco más de resistencia.
            float velocidadAlejamiento = Vector3.Dot(rb.linearVelocity, -direccionHaciaCentro);
            float gravedadFinal = 600f;

            if (velocidadAlejamiento > 0)
            {
                // Aumentamos la gravedad solo mientras sube para frenar el "cohete"
                gravedadFinal = 900f;
            }

            rb.AddForce(direccionHaciaCentro * gravedadFinal, ForceMode.Acceleration);

            // Alineación de rotación
            Quaternion targetRotation = Quaternion.FromToRotation(other.transform.up, -direccionHaciaCentro) * other.transform.rotation;
            other.transform.rotation = Quaternion.Slerp(other.transform.rotation, targetRotation, 10f * Time.deltaTime);
        }
    }
}