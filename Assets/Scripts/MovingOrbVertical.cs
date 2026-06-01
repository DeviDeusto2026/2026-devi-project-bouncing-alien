using UnityEngine;

public class MovingOrbVertical : MonoBehaviour
{
    [Header("Vertical Movement Settings")]
    public float movementDistance = 1.5f;
    public float movementSpeed = 1f;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * movementSpeed) * movementDistance;
        transform.position = initialPosition + Vector3.up * offset;
    }
}