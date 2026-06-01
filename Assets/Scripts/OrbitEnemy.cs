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

        Vector3 localX = orbitCenter.right * xOffset;
        Vector3 localY = orbitCenter.up * yOffset;

        transform.position = orbitCenter.position + localX + localY;
    }
}