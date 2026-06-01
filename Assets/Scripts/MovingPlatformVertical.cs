using UnityEngine;

public class MovingPlatformVertical : MonoBehaviour
{
    public float movementDistance = 5f;
    public float movementSpeed = 1.5f;

    private Vector3 initialPosition;
    private Vector3 previousPosition;

    public Vector3 DeltaMovement { get; private set; }

    void Start()
    {
        initialPosition = transform.position;
        previousPosition = transform.position;
    }

    void FixedUpdate()
    {
        float offset = Mathf.Sin(Time.time * movementSpeed) * movementDistance;

        Vector3 newPosition = initialPosition + (Vector3.up * offset);

        DeltaMovement = newPosition - previousPosition;

        transform.position = newPosition;

        previousPosition = transform.position;
    }
}