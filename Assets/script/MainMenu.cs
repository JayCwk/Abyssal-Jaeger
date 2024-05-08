using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using TMPro;
public class MainMenu : MonoBehaviour
{
    AudioManager audiomg;
    public TextMeshProUGUI coinEarn;

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

    public float showCrytpoEarn()
    {
        if (PlayerPrefs.HasKey("CryptoCurrency"))
        {
            float cryptoEarned = PlayerPrefs.GetFloat("CryptoCurrency");
            coinEarn.text = cryptoEarned.ToString(); // Update the text component with the earned cryptocurrency
            return cryptoEarned;
        }
        else
        {
            coinEarn.text = "0"; // Update the text component with 0 if the key doesn't exist
            return 0f; // Default value if the key doesn't exist
        }
    }
}
