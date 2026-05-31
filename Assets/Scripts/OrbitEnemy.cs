using UnityEngine;

public class OrbitEnemy : MonoBehaviour
{
    public Transform center;
    public float radius = 2f;
    public float speed = 50f;

    private float angle;

    void Start()
    {
        if (center == null)
        {
            Debug.LogWarning("Falta asignar el centro de órbita");
            enabled = false;
        }
    }

    void Update()
    {
        angle += speed * Time.deltaTime;

        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
        float y = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;

        transform.position = center.position + new Vector3(x, y, 0);
    }
}