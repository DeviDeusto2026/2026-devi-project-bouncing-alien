using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [Header("Panels Menu Principal")]
    public GameObject mainMenuPanel;
    public GameObject optionsPanel;

    [Header("Panels Escena de Juego")]
    public GameObject pausePanel; // Asignar solo en la escena de juego

    [Header("Audio Settings")]
    public AudioMixer mainAudioMixer;
    public Slider volumeSlider;
    public Toggle muteToggle;

    [Header("Graphics Settings")]
    public TMP_Dropdown qualityDropdown;

    private bool isPaused = false;
    private AudioSource ambientMusicSource;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        float savedVolume = PlayerPrefs.GetFloat("Volume", 0.75f);
        if (volumeSlider != null) volumeSlider.value = savedVolume;
        SetVolume(savedVolume);

        // Buscamos la música de fondo en la escena (si existe)
        GameObject musicaObj = GameObject.Find("AmbientMusic");
        if (musicaObj != null)
        {
            ambientMusicSource = musicaObj.GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        // Detectamos el botón Escape SOLO si tenemos un panel de pausa asignado en esta escena
        if (pausePanel != null && Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // Navegation Menus
    public void OpenOptions()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (optionsPanel != null) optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        if (optionsPanel != null) optionsPanel.SetActive(false);
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
    }

    // Actions
    public void PlayGame()
    {
        Time.timeScale = 1f; // Descongelamos el tiempo antes de cargar la partida
        SceneManager.LoadScene("MainScene"); // Asegúrate de que se llama así tu escena de juego
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Lógica de Pausa (Para usar en el juego)
    public void PauseGame()
    {
        if (pausePanel == null) return;

        pausePanel.SetActive(true);
        Time.timeScale = 0f; // Congela el juego
        isPaused = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (ambientMusicSource != null && ambientMusicSource.isPlaying)
        {
            ambientMusicSource.Pause(); // Pausa la música relajante
        }
    }

    public void ResumeGame()
    {
        if (pausePanel == null) return;

        pausePanel.SetActive(false);
        Time.timeScale = 1f; // Devuelve el tiempo a la normalidad
        isPaused = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (ambientMusicSource != null && !ambientMusicSource.isPlaying)
        {
            ambientMusicSource.UnPause(); // Reanuda la música
        }
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // ¡Muy importante! Descongelamos el tiempo antes de ir al menú
        SceneManager.LoadScene("MainMenuScene"); // Pon el nombre exacto de tu escena de menú
    }

    // Audio
    public void SetVolume(float volume)
    {
        if (mainAudioMixer != null)
        {
            mainAudioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        }
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }

    public void ToggleMute(bool isTicked)
    {
        if (mainAudioMixer == null) return;

        if (isTicked)
        {
            if (volumeSlider != null) SetVolume(volumeSlider.value);
            PlayerPrefs.SetFloat("MuteMultiplier", 1f);
        }
        else
        {
            mainAudioMixer.SetFloat("MasterVolume", -80f);
            PlayerPrefs.SetFloat("MuteMultiplier", 0f);
        }
        PlayerPrefs.Save();
    }

    // Quality
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("Quality", qualityIndex);
        PlayerPrefs.Save();
    }
}