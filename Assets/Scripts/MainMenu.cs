using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject optionsPanel;

    [Header("Audio Settings")]
    public AudioMixer mainAudioMixer;
    public Slider volumeSlider;

    [Header("Graphics Settings")]
    public TMP_Dropdown qualityDropdown;

    public Toggle muteToggle;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        float savedVolume = PlayerPrefs.GetFloat("Volume", 0.75f);
        volumeSlider.value = savedVolume;
        SetVolume(savedVolume);
    }

    // Navegation
    public void OpenOptions()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    // Actions
    public void PlayGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Audio
    public void SetVolume(float volume)
    {
        mainAudioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save(); // Forzamos el guardado
    }

    public void ToggleMute(bool isTicked)
    {
        if (isTicked) // Si el Toggle está activado (escuchamos el juego)
        {
            SetVolume(volumeSlider.value);
            PlayerPrefs.SetFloat("MuteMultiplier", 1f); // 1 significa sonido normal
        }
        else // Si el Toggle está desactivado (queremos silencio)
        {
            mainAudioMixer.SetFloat("MasterVolume", -80f);
            PlayerPrefs.SetFloat("MuteMultiplier", 0f); // 0 significa silencio absoluto
        }
        PlayerPrefs.Save(); // Forzamos el guardado
    }

    // Quality
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("Quality", qualityIndex);
        PlayerPrefs.Save();
    }
}