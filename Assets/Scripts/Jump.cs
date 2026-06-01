using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Jump : MonoBehaviour
{
    public float movementSpeed = 10f;
    public float jumpForce = 30f;

    public Transform currentPlatformZone;

    [Header("Audio Settings")]
    public AudioSource playerAudioSource;
    public AudioClip jumpSound;

    private Rigidbody rb;
    private bool isGrounded;
    private Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = false;
    }

    void Update()
    {
        HandleMovementInput();
        HandleJump();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void HandleMovementInput()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector3 cameraRight = Vector3.ProjectOnPlane(Camera.main.transform.right, transform.up).normalized;
        moveDirection = cameraRight * horizontalInput;
    }

    private void MovePlayer()
    {
        Vector3 horizontalVelocity = moveDirection * movementSpeed;
        Vector3 verticalVelocity = Vector3.Project(rb.linearVelocity, transform.up);
        rb.linearVelocity = horizontalVelocity + verticalVelocity;
    }

    private void HandleJump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            currentPlatformZone = null;
            transform.SetParent(null);

            rb.linearVelocity = Vector3.ProjectOnPlane(rb.linearVelocity, transform.up);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

            if (playerAudioSource != null && jumpSound != null)
            {
                playerAudioSource.PlayOneShot(jumpSound);
            }

            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.GetComponent<MovingPlatform>() != null ||
            collision.gameObject.GetComponent<MovingPlatformVertical>() != null)
        {
            transform.SetParent(collision.transform);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            isGrounded = false;
        }

        if (collision.gameObject.GetComponent<MovingPlatform>() != null ||
            collision.gameObject.GetComponent<MovingPlatformVertical>() != null)
        {
            transform.SetParent(null);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Platform>() != null)
        {
            currentPlatformZone = collider.transform;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.GetComponent<Platform>() != null && currentPlatformZone == collider.transform)
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

        isGrounded = false;
    }
}