using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class ReplayButton : MonoBehaviour
{
    public string gameSceneName = "GameScene"; // Name of your game scene
    public string gameSceneName1 = "GameScene"; // Name of your game scene
    AudioManager audiomg;

    private void Start()
    {
        audiomg = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public void ReplayGame()
    {
        audiomg.PlaySFX(audiomg.OnClicked);
        SceneManager.LoadScene(gameSceneName); // Load the game scene again
        PlayerCtrl.ResetPlayerPrefs();
    }

    public void HomeGame()
    {
        audiomg.PlaySFX(audiomg.Cancel);
        SceneManager.LoadScene(gameSceneName1); // Load the game scene again
        PlayerCtrl.ResetPlayerPrefs();
    }
}
