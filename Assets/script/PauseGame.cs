using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    public GameObject settingsPanel; // Renamed variable for clarity
    public GameObject darkOverlay; // Reference to the dark overlay image
    private bool isPaused = false;

    AudioManager audiomg;

    void Start()
    {
        settingsPanel.SetActive(false); // Hide the settings panel initially
        if (darkOverlay != null)
        {
            darkOverlay.SetActive(false); // Hide the dark overlay initially
        }

        audiomg = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Changed to use KeyCode.Escape for better clarity
        {
            ToggleSettingsPanel(); // Toggle the settings panel when the Escape key is pressed
        }
    }

    public void ToggleSettingsPanel()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f; // Pause/unpause the game
        audiomg.PlaySFX(audiomg.Pause);

        if (settingsPanel != null)
        {
            settingsPanel.SetActive(isPaused); // Activate/deactivate the settings panel based on the pause state
        }

        if (darkOverlay != null)
        {
            darkOverlay.SetActive(isPaused); // Activate/deactivate the dark overlay based on the pause state
        }
    }
}
