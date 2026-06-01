using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter(Collider collider)
    {
        PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }
}