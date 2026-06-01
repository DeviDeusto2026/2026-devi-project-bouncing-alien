using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Jump : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 30f;

    // Variable para guardar la zona de gravedad actual
    public Transform currentPlatformZone;

    [Header("Configuración de Sonido")]
    public AudioSource playerAudioSource;
    public AudioClip jumpSound;

    private Rigidbody rb;
    private bool canJump;
    private Vector3 moveDir;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = false;
    }

    void Update()
    {
        MovementInput();
        JumpIfNeeded();
    }

    void FixedUpdate()
    {
        Move();
    }

    void MovementInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        Vector3 camRight = Vector3.ProjectOnPlane(Camera.main.transform.right, transform.up).normalized;
        moveDir = camRight * h;
    }

    void Move()
    {
        Vector3 horizontalVelocity = moveDir * speed;
        Vector3 verticalVelocity = Vector3.Project(rb.linearVelocity, transform.up);
        rb.linearVelocity = horizontalVelocity + verticalVelocity;
    }

    void JumpIfNeeded()
    {
        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {
            currentPlatformZone = null;
            transform.SetParent(null);

            rb.linearVelocity = Vector3.ProjectOnPlane(rb.linearVelocity, transform.up);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);


            if (playerAudioSource != null && jumpSound != null)
            {
                playerAudioSource.PlayOneShot(jumpSound);
            }

            canJump = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            canJump = true;
        }

        if (collision.gameObject.GetComponent<MovingPlatform>() != null ||
            collision.gameObject.GetComponent<MovingPlatformVertical>() != null)
        {
            transform.SetParent(collision.transform);
        }
    }

    void OnCollisionStay(Collision collision)
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

        if (collision.gameObject.GetComponent<MovingPlatform>() != null ||
            collision.gameObject.GetComponent<MovingPlatformVertical>() != null)
        {
            transform.SetParent(null);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Platform>() != null)
        {
            currentPlatformZone = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Platform>() != null && currentPlatformZone == other.transform)
        {
            currentPlatformZone = null;
        }
    }

    public void OrbJump(float orbForce)
    {
        currentPlatformZone = null;
        transform.SetParent(null);

        rb.linearVelocity = Vector3.ProjectOnPlane(rb.linearVelocity, transform.up);
        rb.AddForce(transform.up * orbForce, ForceMode.Impulse);

        canJump = false;
    }
}