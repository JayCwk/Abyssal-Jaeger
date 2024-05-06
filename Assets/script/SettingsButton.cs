using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsButton : MonoBehaviour
{
    public GameObject settingsPanel;
    private bool isPaused = false;
    AudioManager audiomg;

    void Start()
    {
        settingsPanel.SetActive(false); // Hide the settings panel initially
        audiomg = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isPaused) // Detect if the user presses the escape key
        {
            ResumeGame(); // Resume the game if the settings panel is open and the user presses escape
        }
    }

    public void ToggleSettingsPanel()
    {
        if (settingsPanel != null)
        {
            audiomg.PlaySFX(audiomg.OnClicked);
            isPaused = !isPaused;
            settingsPanel.SetActive(isPaused); // Show/hide the settings panel
            Time.timeScale = isPaused ? 0f : 1f; // Pause/unpause the game
        }
    }

    public void ResumeGame()
    {
        audiomg.PlaySFX(audiomg.OnClicked);
        isPaused = false;
        settingsPanel.SetActive(false); // Hide the settings panel
        Time.timeScale = 1f; // Resume the game
    }
}
