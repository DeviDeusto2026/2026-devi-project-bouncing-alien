using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Jump : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 18f;

    private Rigidbody rb;
    private bool canJump;
    private Vector3 moveDir;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        // IMPORTANTE: la gravedad la hace el planeta, no Unity
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
        float v = Input.GetAxisRaw("Vertical");

        Vector3 camForward = Vector3.ProjectOnPlane(Camera.main.transform.forward, transform.up).normalized;
        Vector3 camRight = Vector3.ProjectOnPlane(Camera.main.transform.right, transform.up).normalized;

        moveDir = (camForward * v + camRight * h).normalized;

        if (moveDir.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir, transform.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 8f * Time.deltaTime);
        }
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
            rb.linearVelocity = Vector3.ProjectOnPlane(rb.linearVelocity, transform.up);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            canJump = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            canJump = true;
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
    }
}