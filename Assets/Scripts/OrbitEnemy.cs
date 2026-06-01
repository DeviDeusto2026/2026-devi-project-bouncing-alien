using UnityEngine;

public class OrbitEnemy : MonoBehaviour
{
    public Transform orbitCenter;
    public float orbitRadius = 2f;
    public float orbitSpeed = 50f;

    private float currentAngle;

    void Start()
    {
        if (orbitCenter == null)
        {
            Debug.LogWarning("OrbitCenter is not assigned on " + gameObject.name);
            enabled = false;
        }
    }

    void Update()
    {
        currentAngle += orbitSpeed * Time.deltaTime;

        float xOffset = Mathf.Cos(currentAngle * Mathf.Deg2Rad) * orbitRadius;
        float yOffset = Mathf.Sin(currentAngle * Mathf.Deg2Rad) * orbitRadius;

        transform.position = orbitCenter.position + (Vector3.right * xOffset) + (Vector3.up * yOffset);
    }
}