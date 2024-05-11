using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI cryptoCurrency;

    AudioManager audiomg;

    void Start()
    {
        // Get the score from the GameManager
        int score = GameManger.instance.score;

        // Calculate the cryptocurrency value
        float cryptocurrencyValue = score * 0.002f;

        // Load the saved cryptocurrency value
        float savedCryptoValue = LoadCryptoCurrency();

        // Add the newly earned cryptocurrency to the saved value
        savedCryptoValue += cryptocurrencyValue;

        // Save the updated cryptocurrency value (optional: save only if it has changed significantly)
        SaveCryptoCurrency(savedCryptoValue);

        // Display the final score
        finalScoreText.text = "Final Score: " + score.ToString();

        // Display the total cryptocurrency value
        cryptoCurrency.text = "CC earned: " + cryptocurrencyValue.ToString("F2");

        audiomg = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void SaveCryptoCurrency(float value)
    {
        PlayerPrefs.SetFloat("CryptoCurrency", value);
        PlayerPrefs.Save(); // Ensure that the data is immediately written to disk
    }

    public float LoadCryptoCurrency()
    {
        if (PlayerPrefs.HasKey("CryptoCurrency"))
        {
            return PlayerPrefs.GetFloat("CryptoCurrency");
        }
        else
        {
            return 0f; // Default value if the key doesn't exist
        }
    }

    // Method to go back to the main menu or home screen
    public void Home()
    {
        audiomg.PlaySFX(audiomg.OnClicked);
        SceneManager.LoadScene("Start"); // Load your main menu scene here
    }

    // Method to replay the game
    public void Replay()
    {
        audiomg.PlaySFX(audiomg.OnClicked);
        // Reset specific keys relevant to player preferences
        PlayerPrefs.DeleteKey("PlayerSharedHealth");
        
        SceneManager.LoadScene("GameScene"); // Load your game scene here
    }

   
}


