using UnityEngine;
using System.Collections;

public class DisappearAfterTouch : MonoBehaviour
{
    public float delayBeforeDisappear = 1f;
    public float hiddenDuration = 2f;

    private Renderer[] platformRenderers;
    private Collider[] platformColliders;
    private bool isActivated = false;

    void Start()
    {
        platformRenderers = GetComponentsInChildren<Renderer>();
        platformColliders = GetComponentsInChildren<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isActivated) return;

        Jump player = collision.gameObject.GetComponent<Jump>();
        if (player != null)
        {
            isActivated = true;
            StartCoroutine(DisappearRoutine());
        }
    }

    private IEnumerator DisappearRoutine()
    {
        yield return new WaitForSeconds(delayBeforeDisappear);

        SetPlatformVisibility(false);

        yield return new WaitForSeconds(hiddenDuration);

        SetPlatformVisibility(true);
        isActivated = false;
    }

    private void SetPlatformVisibility(bool isVisible)
    {
        foreach (Renderer rendererComponent in platformRenderers)
        {
            rendererComponent.enabled = isVisible;
        }

        foreach (Collider colliderComponent in platformColliders)
        {
            colliderComponent.enabled = isVisible;
        }
    }
}