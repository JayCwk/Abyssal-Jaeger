using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI cryptoCurrency;

    public void Start()
    {
        // Get the score from the GameManager
        int score = GameManger.instance.score;

        // Calculate the cryptocurrency value
        float cryptocurrencyValue = score * 0.02f;

        // Display the final score
        finalScoreText.text = "Final Score: " + score.ToString();

        // Display the cryptocurrency value
        cryptoCurrency.text = "Crypto  Currency: " + cryptocurrencyValue.ToString();
    }
}
