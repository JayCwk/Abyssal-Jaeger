using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject PausingGame;
    private bool isPaused = false;

    void Start()
    {
        PausingGame.SetActive(false); // Hide the settings panel initially
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isPaused) // Detect if the user presses the escape key
        {
            ToggleSettingsPanel(); // Resume the game if the settings panel is open and the user presses escape
        }
    }

    public void ToggleSettingsPanel()
    {
        if (PausingGame != null)
        {
            isPaused = !isPaused;
            PausingGame.SetActive(isPaused); // Show/hide the settings panel
            Time.timeScale = isPaused ? 0f : 1f; // Pause/unpause the game
        }
    }

  
}
