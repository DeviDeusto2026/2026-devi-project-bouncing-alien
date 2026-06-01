using UnityEngine;
using System.Collections;

public class BossHealth : MonoBehaviour
{
    public int currentHealth = 3;
    public float invulnerabilityDuration = 1f;

    [Header("Victory UI Settings")]
    public GameObject victoryPanel;

    private Renderer[] bossRenderers;
    private Collider[] bossColliders;
    private AudioSource ambientMusicSource;
    private bool isInvulnerable = false;

    void Start()
    {
        bossRenderers = GetComponentsInChildren<Renderer>();
        bossColliders = GetComponentsInChildren<Collider>();

        GameObject musicObject = GameObject.Find("AmbientMusic");
        if (musicObject != null)
        {
            ambientMusicSource = musicObject.GetComponent<AudioSource>();
        }
    }

    public void TakeHit()
    {
        if (isInvulnerable) return;

        currentHealth--;

        if (currentHealth <= 0)
        {
            HandleVictory();
            return;
        }

        StartCoroutine(BecomeTemporarilyInvulnerable());
    }

    private void HandleVictory()
    {
        if (ambientMusicSource != null)
        {
            ambientMusicSource.Pause();
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        victoryPanel.SetActive(true);
        Time.timeScale = 0f;
        gameObject.SetActive(false);
    }

    private IEnumerator BecomeTemporarilyInvulnerable()
    {
        isInvulnerable = true;

        SetBossVisibility(false);

        yield return new WaitForSeconds(invulnerabilityDuration);

        SetBossVisibility(true);

        isInvulnerable = false;
    }

    private void SetBossVisibility(bool isVisible)
    {
        foreach (Renderer rendererComponent in bossRenderers)
        {
            rendererComponent.enabled = isVisible;
        }

        foreach (Collider colliderComponent in bossColliders)
        {
            colliderComponent.enabled = isVisible;
        }
    }
}