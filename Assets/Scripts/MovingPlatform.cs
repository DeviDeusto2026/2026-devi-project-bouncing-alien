using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float movementDistance = 6f;
    public float movementSpeed = 1.5f;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * movementSpeed) * movementDistance;
        transform.position = initialPosition + Vector3.right * offset;
    }
}