
using UnityEngine;

public class Jump : MonoBehaviour
{
    public int jumpForce = 800; // Subido para compensar la gravedad planetaria
    public int speed = 10;
    private bool canJump;

    void Update()
    {
        Movement();
        JumIsNeeded();
    }

    void Movement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Calculamos direcciones relativas a la cámara y al suelo
        Vector3 camForward = Vector3.ProjectOnPlane(Camera.main.transform.forward, transform.up).normalized;
        Vector3 camRight = Vector3.ProjectOnPlane(Camera.main.transform.right, transform.up).normalized;

        Vector3 moveDir = (camForward * v + camRight * h).normalized;

        if (moveDir.magnitude > 0.1f)
        {
            // 1. MOVER: El movimiento siempre es fluido
            transform.position += moveDir * speed * Time.deltaTime;

            // 2. ROTACIÓN INTELIGENTE:
            // Solo permitimos que el Alien rote si hay un movimiento lateral (A o D) significativo.
            // Si solo pulsas W o S, el personaje mantendrá su rotación actual.
            if (Mathf.Abs(h) > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDir, transform.up);
                // Bajamos a 2f para que el giro sea extremadamente suave
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2f * Time.deltaTime);
            }
        }
    }

    private void JumIsNeeded()
    {
        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {
            // Usamos transform.up porque el "arriba" del mundo ya no sirve en el planeta
            this.GetComponent<Rigidbody>().AddForce(transform.up * jumpForce);
        }
    }

    // Detección de suelo usando la capa "ground"
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            canJump = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            canJump = false;
        }
    }
}