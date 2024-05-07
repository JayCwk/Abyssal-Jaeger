using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    AudioManager audiomg;

    private void Start()
    {
        PlayerCtrl.ResetPlayerPrefs();
        audiomg = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public void PlayGame()
    {
       
        SceneManager.LoadSceneAsync(1);
    }

    public void PlayGame1()
    {
        
        SceneManager.LoadSceneAsync(2);
    }
    public void PlayCustomizationProfile()
    {
        
        SceneManager.LoadSceneAsync(3);
    }

    public void QuitGame()
    {
        PlayerCtrl.ResetPlayerPrefs();
        Application.Quit();
    }
}
