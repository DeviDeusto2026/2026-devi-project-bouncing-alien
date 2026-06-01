using UnityEngine;

public class BossWeakPoint : MonoBehaviour
{
    public BossHealth bossHealth;

    private void OnTriggerEnter(Collider other)
    {
        Jump player = other.GetComponent<Jump>();

        if (player != null && bossHealth != null)
        {
            bossHealth.TakeHit();
        }
    }
}
