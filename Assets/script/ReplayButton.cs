using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReplayButton : MonoBehaviour
{
    public string gameSceneName = "GameScene"; // Name of your game scene
    public string gameSceneName1 = "GameScene"; // Name of your game scene

    public void ReplayGame()
    {
        SceneManager.LoadScene(gameSceneName); // Load the game scene again
        PlayerCtrl.ResetPlayerPrefs();
    }

    public void HomeGame()
    {
        SceneManager.LoadScene(gameSceneName1); // Load the game scene again
        PlayerCtrl.ResetPlayerPrefs();
    }
}
