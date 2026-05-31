using UnityEngine;
using UnityEngine.UI;

public class HealthHUDManager : MonoBehaviour
{
    [Header("UI Elements")]
    [Tooltip("Array containing the heart images in order (0, 1, 2)")]
    public Image[] heartImages;

    public void UpdateHeartsHUD(int currentLives)
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < currentLives)
            {
                heartImages[i].enabled = true;
            }
            else
            {
                heartImages[i].enabled = false;
            }
        }
    }
}