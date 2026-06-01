using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 3;
    public Transform respawnPoint;

    [Header("Linked HUD")]
    [Tooltip("Reference to the HUD manager script in the Canvas")]
    public HealthHUDManager hudManager;

    [Header("Game Over Settings")]
    [Tooltip("Reference to the Game Over Panel in the Canvas")]
    public GameObject gameOverPanel;

    private int currentHealth;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        InitializeHealth();
    }

    private void InitializeHealth()
    {
        currentHealth = maxHealth;

        if (hudManager != null)
        {
            hudManager.UpdateHeartsHUD(currentHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (hudManager != null)
        {
            hudManager.UpdateHeartsHUD(currentHealth);
        }

        if (currentHealth <= 0)
        {
            TriggerGameOver();
        }
    }

    public void Respawn()
    {
        InitializeHealth();

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

    private void TriggerGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}