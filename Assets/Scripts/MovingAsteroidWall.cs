using UnityEngine;

public class MovingAsteroidWall : MonoBehaviour
{
    [Header("Movimiento lateral")]
    public float distance = 4f;
    public float speed = 1f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * speed) * distance;
        transform.position = startPosition + Vector3.right * offset;
    }
}
