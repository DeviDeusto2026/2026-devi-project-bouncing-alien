using UnityEngine;

public class MovingPlatformVertical : MonoBehaviour
{
    public float distance = 5f;
    public float speed = 1.5f;

    private Vector3 startPosition;
    private Vector3 lastPosition;

    public Vector3 DeltaMovement { get; private set; }

    void Start()
    {
        startPosition = transform.position;
        lastPosition = transform.position;
    }

    void FixedUpdate()
    {
        float movement = Mathf.Sin(Time.time * speed) * distance;

        // Movimiento recto arriba/abajo en el mundo
        Vector3 newPosition = startPosition + new Vector3(0f, movement, 0f);

        DeltaMovement = newPosition - lastPosition;

        transform.position = newPosition;

        lastPosition = transform.position;
    }
}
