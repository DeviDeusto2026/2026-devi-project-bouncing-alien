using UnityEngine;

public class BossWeakPoint : MonoBehaviour
{
    public BossHealth bossHealth;

    private void OnTriggerEnter(Collider collider)
    {
        Jump player = collider.GetComponent<Jump>();

        if (player != null && bossHealth != null)
        {
            bossHealth.TakeHit();
        }
    }
}