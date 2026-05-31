using UnityEngine;

public class MovingOrbVertical : MonoBehaviour
{
    [Header("Movimiento vertical")]
    public float distance = 1.5f;
    public float speed = 1f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * speed) * distance;
        transform.position = startPosition + Vector3.up * offset;
    }
}
