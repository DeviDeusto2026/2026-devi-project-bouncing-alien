using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [Header("Main Menu Panels")]
    public GameObject mainMenuPanel;
    public GameObject optionsPanel;

    [Header("Gameplay Panels")]
    public GameObject pausePanel;

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
        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
        }
        SetVolume(savedVolume);

        GameObject musicObject = GameObject.Find("AmbientMusic");
        if (musicObject != null)
        {
            ambientMusicSource = musicObject.GetComponent<AudioSource>();
        }
    }

    void Update()
    {
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

    // Menu Navigation
    public void OpenOptions()
    {
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(false);
        }
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(true);
        }
    }

    public void CloseOptions()
    {
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(false);
        }
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(true);
        }
    }

    // Actions
    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Pause Logic
    public void PauseGame()
    {
        if (pausePanel == null) return;

        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (ambientMusicSource != null && ambientMusicSource.isPlaying)
        {
            ambientMusicSource.Pause();
        }
    }

    public void ResumeGame()
    {
        if (pausePanel == null) return;

        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (ambientMusicSource != null && !ambientMusicSource.isPlaying)
        {
            ambientMusicSource.UnPause();
        }
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");
    }

    // Audio Controls
    public void SetVolume(float volume)
    {
        if (mainAudioMixer != null)
        {
            mainAudioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        }
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }

    public void ToggleMute(bool isSoundEnabled)
    {
        if (mainAudioMixer == null) return;

        if (isSoundEnabled)
        {
            if (volumeSlider != null)
            {
                SetVolume(volumeSlider.value);
            }
            PlayerPrefs.SetFloat("MuteMultiplier", 1f);
        }
        else
        {
            mainAudioMixer.SetFloat("MasterVolume", -80f);
            PlayerPrefs.SetFloat("MuteMultiplier", 0f);
        }
        PlayerPrefs.Save();
    }

    // Graphics Quality
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("Quality", qualityIndex);
        PlayerPrefs.Save();
    }
}