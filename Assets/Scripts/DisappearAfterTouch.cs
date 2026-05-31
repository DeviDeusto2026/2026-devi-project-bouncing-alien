using UnityEngine;
using System.Collections;

public class DisappearAfterTouch : MonoBehaviour
{
    public float delayBeforeDisappear = 1f;
    public float timeHidden = 2f;

    private Renderer[] renderers;
    private Collider[] colliders;
    private bool activated = false;

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        colliders = GetComponentsInChildren<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (activated) return;

        if (collision.gameObject.GetComponent<Jump>() != null)
        {
            activated = true;
            StartCoroutine(DisappearRoutine());
        }
    }

    IEnumerator DisappearRoutine()
    {
        yield return new WaitForSeconds(delayBeforeDisappear);

        SetPlatform(false);

        yield return new WaitForSeconds(timeHidden);

        SetPlatform(true);
        activated = false;
    }

    void SetPlatform(bool state)
    {
        foreach (Renderer r in renderers)
            r.enabled = state;

        foreach (Collider c in colliders)
            c.enabled = state;
    }
}
