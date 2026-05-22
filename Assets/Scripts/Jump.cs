using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Jump : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 30f;

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