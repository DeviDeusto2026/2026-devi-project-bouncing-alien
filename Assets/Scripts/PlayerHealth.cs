using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;
    public Transform respawnPoint;

    private int currentLives;
    private Rigidbody rb;

    void Start()
    {
        currentLives = maxLives;
        rb = GetComponent<Rigidbody>();
    }

    public void TakeDamage(int damage)
    {
        currentLives -= damage;
        Debug.Log("Vidas restantes: " + currentLives);

        if (currentLives <= 0)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        currentLives = maxLives;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position;
        }
    }
}
