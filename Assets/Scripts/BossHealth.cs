using UnityEngine;
using System.Collections;

public class BossHealth : MonoBehaviour
{
    public int lives = 3;
    public float hideTime = 1f;

    private Renderer[] renderers;
    private Collider[] colliders;
    private bool canTakeDamage = true;

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        colliders = GetComponentsInChildren<Collider>();
    }

    public void TakeHit()
    {
        if (!canTakeDamage) return;

        lives--;

        if (lives <= 0)
        {
            gameObject.SetActive(false);
            return;
        }

        StartCoroutine(HideAndReturn());
    }

    IEnumerator HideAndReturn()
    {
        canTakeDamage = false;

        SetBossVisible(false);

        yield return new WaitForSeconds(hideTime);

        SetBossVisible(true);

        canTakeDamage = true;
    }

    void SetBossVisible(bool state)
    {
        foreach (Renderer r in renderers)
            r.enabled = state;

        foreach (Collider c in colliders)
            c.enabled = state;
    }
}