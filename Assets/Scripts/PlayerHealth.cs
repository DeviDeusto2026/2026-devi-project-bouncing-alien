using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxLives = 3;
    public Transform respawnPoint;

    [Header("Linked HUD")]
    [Tooltip("Reference to the HUD manager script in the Canvas")]
    public HealthHUDManager hudManager;

    private int currentLives;
    private Rigidbody rb;

    [Header("Game Over Settings")]
    [Tooltip("Reference to the Game Over Panel in the Canvas")]
    public GameObject gameOverPanel;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        InitializeHealth();
    }

    private void InitializeHealth()
    {
        currentLives = maxLives;

        if (hudManager != null)
        {
            hudManager.UpdateHeartsHUD(currentLives);
        }
    }

    public void TakeDamage(int damage)
    {
        currentLives -= damage;
        Debug.Log("Remaining lives: " + currentLives);

        if (hudManager != null)
        {
            hudManager.UpdateHeartsHUD(currentLives);
        }

        if (currentLives <= 0)
        {
            TriggerGameOver();
        }
    }

    void Respawn()
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

    void TriggerGameOver()
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
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}