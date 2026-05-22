using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float distance = 6f;
    public float speed = 1.5f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float movement = Mathf.Sin(Time.time * speed) * distance;

        // Movimiento recto izquierda/derecha en el mundo
        transform.position = startPosition + new Vector3(movement, 0f, 0f);
    }
}