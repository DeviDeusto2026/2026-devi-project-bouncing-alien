using UnityEngine;

public class MovingAsteroidWall : MonoBehaviour
{
    [Header("Lateral Movement Settings")]
    public float movementDistance = 4f;
    public float movementSpeed = 1f;

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