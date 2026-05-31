using UnityEngine;

public class MovingEnemyHorizontal : MonoBehaviour
{
    public float distance = 3f;
    public float speed = 1.5f;

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
