using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8f;
    public int damage = 1;
    public float lifeTime = 5f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider collider)
    {
        PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}