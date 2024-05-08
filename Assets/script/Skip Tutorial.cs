using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SkipTutorial : MonoBehaviour
{
    public GameObject skipPanel; // Reference for skip panel
    public GameObject darkOverlay; // Reference to the dark overlay image
    private bool isPaused = false;

    AudioManager audioSource;

    // Start is called before the first frame update
    void Start()
    {
        skipPanel.SetActive(false); // Hide the skip panel initially
        if (darkOverlay != null)
        {
            darkOverlay.SetActive(false); // Hide the dark overlay initially
        }

        audioSource = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void skipTutorial()
    {
        isPaused = true;
        Time.timeScale = 0f;    //0f : 1f; Pause : unpause the game
        audioSource.PlaySFX(audioSource.Pause);

        if (skipPanel != null)
        {
            skipPanel.SetActive(isPaused); // Activate/deactivate the settings panel based on the pause state
        }

        if (darkOverlay != null)
        {
            darkOverlay.SetActive(isPaused); // Activate/deactivate the dark overlay based on the pause state
        }
    }

    public void Cancel()
    {
        isPaused = false;
        Time.timeScale = 1f;
        audioSource.PlaySFX(audioSource.Pause);

        if (skipPanel != null)
        {
            skipPanel.SetActive(isPaused); // Activate/deactivate the settings panel based on the pause state
        }

        if (darkOverlay != null)
        {
            darkOverlay.SetActive(isPaused); // Activate/deactivate the dark overlay based on the pause state
        }
    }

    public void MainMenu()
    {
        isPaused = false;
        Time.timeScale = 1f;
        audioSource.PlaySFX(audioSource.Pause);
        PlayerPrefs.DeleteKey("PlayerSharedHealth");
        SceneManager.LoadScene("Start"); // Load main menu scene 
    }
}
